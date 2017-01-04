//----------------------------------------------
//            NGUI: Next-Gen UI kit
// Copyright © 2011-2015 Tasharen Entertainment
//----------------------------------------------

using UnityEngine;
using LuaInterface;
using System.Collections;

[AddComponentMenu("CLFrame/Drag and Drop Item (CLFrame)")]
public class CLUIDragDropItem : UIDragDropItem
{
#region Delegates	
	public delegate void ObjectCall (GameObject go, GameObject obj);
	public delegate void VoidCall (GameObject go);
#endregion

	public VoidCall mOnDragStart;
	public VoidCall mOnDragging;
	public ObjectCall mOnClone;
	public ObjectCall mOnDragDropEnd;

	protected CLUIDragDropItem mCloneFrom = null;

	protected virtual void OnDragDropStart ()
	{
		// Automatically disable the scroll view
		if (mDragScrollView != null) mDragScrollView.enabled = false;

		// Disable the collider so that it doesn't intercept events
		if (mButton != null) mButton.isEnabled = false;
		else if (mCollider != null) mCollider.enabled = false;
		else if (mCollider2D != null) mCollider2D.enabled = false;

		mParent = mTrans.parent;
		mRoot = NGUITools.FindInParents<UIRoot>(mParent);

		// 为了避免 CLULoopGrid 失效，这里去掉对mGrid的处理
		// mGrid = NGUITools.FindInParents<UIGrid>(mParent);
		// mTable = NGUITools.FindInParents<UITable>(mParent);

		// Re-parent the item
		if (UIDragDropRoot.root != null)
			mTrans.parent = UIDragDropRoot.root;

		Vector3 pos = mTrans.localPosition;
		pos.z = 0f;
		mTrans.localPosition = pos;

		TweenPosition tp = GetComponent<TweenPosition>();
		if (tp != null) tp.enabled = false;

		SpringPosition sp = GetComponent<SpringPosition>();
		if (sp != null) sp.enabled = false;

		// Notify the widgets that the parent has changed
		NGUITools.MarkParentAsChanged(gameObject);

		// if (mTable != null) mTable.repositionNow = true;
		// if (mGrid != null) mGrid.repositionNow = true;
	}	
	/// <summary>
	/// Drop object onto the surface.
	/// </summary>
	protected override void OnDragDropRelease (GameObject surface)
	{
		// Is there a droppable container?
		UIDragDropContainer container = surface ? NGUITools.FindInParents<UIDragDropContainer>(surface) : null;

		if (!cloneOnDrag)
		{
			// Re-enable the collider
			if (mButton != null) mButton.isEnabled = true;
			else if (mCollider != null) mCollider.enabled = true;
			else if (mCollider2D != null) mCollider2D.enabled = true;
			
			if (container != null)
			{
				// Container found -- parent this object to the container
				mTrans.parent = (container.reparentTarget != null) ? container.reparentTarget : container.transform;
				
				Vector3 pos = mTrans.localPosition;
				pos.z = 0f;
				mTrans.localPosition = pos;
			}
			else
			{
				// No valid container under the mouse -- revert the item's parent
				mTrans.parent = mParent;
			}
			
			// Update the grid and table references
			mParent = mTrans.parent;
			// mGrid = NGUITools.FindInParents<UIGrid>(mParent);
			// mTable = NGUITools.FindInParents<UITable>(mParent);
			
			// Re-enable the drag scroll view script
			if (mDragScrollView != null)
				StartCoroutine(EnableDragScrollView());
			
			// Notify the widgets that the parent has changed
			NGUITools.MarkParentAsChanged(gameObject);
			
			// if (mTable != null) mTable.repositionNow = true;
			// if (mGrid != null) mGrid.repositionNow = true;
			
			// We're now done
			OnDragDropEnd( container ? container.gameObject : null );
		}
		else
		{
			// We're now done
			OnDragDropEnd( container ? container.gameObject : null );

			NGUITools.Destroy(gameObject);
		}
	}

	protected override void StartDragging ()
	{
		if (!interactable) return;
		
		if (!mDragging)
		{
			if (cloneOnDrag)
			{
				mPressed = false;
				GameObject clone = NGUITools.AddChild(transform.parent.gameObject, gameObject);
				clone.transform.localPosition = transform.localPosition;
				clone.transform.localRotation = transform.localRotation;
				clone.transform.localScale = transform.localScale;
				
				UIButtonColor bc = clone.GetComponent<UIButtonColor>();
				if (bc != null) bc.defaultColor = GetComponent<UIButtonColor>().defaultColor;
				
				if (mTouch != null && mTouch.pressed == gameObject)
				{
					mTouch.current = clone;
					mTouch.pressed = clone;
					mTouch.dragged = clone;
					mTouch.last = clone;
				}
				
				CLUIDragDropItem item = clone.GetComponent<CLUIDragDropItem>();
				item.mTouch = mTouch;
				item.mPressed = true;
				item.mDragging = true;
				item.mCloneFrom = this;

				if (mOnClone != null) { mOnClone(gameObject, clone); }
			
				item.Start();
				item.OnDragDropStart();

				if (UICamera.currentTouch == null)
					UICamera.currentTouch = mTouch;
				
				mTouch = null;
				
				UICamera.Notify(gameObject, "OnPress", false);
				UICamera.Notify(gameObject, "OnHover", false);
			}
			else
			{
				mDragging = true;
				base.OnDragDropStart();
			}
		}

		if (mOnDragStart != null)
		{
			mOnDragStart( mCloneFrom ? mCloneFrom.gameObject : gameObject );
		}
	}
	
	protected virtual void OnDragDropEnd (GameObject container)
	{
		if (mOnDragDropEnd != null) {
			mOnDragDropEnd( mCloneFrom ? mCloneFrom.gameObject : gameObject, container );
		}
		else if (mCloneFrom != null && mCloneFrom.mOnDragDropEnd != null ) {
			mCloneFrom.mOnDragDropEnd(mCloneFrom.gameObject, container );
		}
	}

	protected override void OnDragDropMove (Vector2 delta)
	{
		base.OnDragDropMove( delta );
		if (mOnDragging != null)
		{
			mOnDragging(mCloneFrom ? mCloneFrom.gameObject : gameObject);
		}
	}
	
}
