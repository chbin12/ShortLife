LJ�   4   % > 4  7  4 +  7> 4  7  4 +  7> 4  7  4 +  7	> 4  7  4
 +  7> G  �OnDisconnectDisconnectOnExceptionExceptionOnLogin
LoginOnConnectConnectAddListener
EventNetwork.Start!!	warn>   4  74   > >G  tostringBrocast
Event4    4   % > G  Game Server connected!!	warna   	/  4     7  > 4  % > G  �OnException------->>>>
errorSendConnectNetManager;   /  4   % > G  �OnDisconnect------->>>>
error�  24  4 7 T�+  7  >4  4 7 T�+  7  >4  4 7 T�+  7  >4  4 7 T�+  7	  >4
 74 7>
  T� 7>4 % >G  �OnLogin----------->>>	warn
AwakeMessageCtrlNameGetCtrlCtrlManagerTestLoginSprotoSPROTOTestLoginPbcPBCTestLoginPbluaPB_LUATestLoginBinaryBINARYProtocalTypeTestProtoTypez     7  >  7 >4 %  %  $>G   str:> TestLoginBinary: protocal:>logReadStringReadByte� 	 	   7  >  7 >4 7> 7 >4 %  % 7$>G  id msg:>TestLoginPblua: protocal:>logParseFromStringLoginResponselogin_pbReadBufferReadByte�   7  7  >  7 >4 7% $4 7 % > 7%	 > 7
>4 7 >4 7%  >4 7>4 7>4 7>T
�4 % 7$7>A
N
�4 % 	 $	>G  TestLoginPbc: protocal:>log	typenumber	
phoneipairsid	name
printtutorial.Persondecoderegisterprotobuf
close*a	readrb	openiolua/3rd/pbc/addressbook.pbDataPath	UtilReadBufferReadByte�    7  >  7 >+  7% > 7%  >+  >4 %  $>G   �� TestLoginSproto: protocal:>logAddressBookdecode�    .Person {
        name 0 : string
        id 1 : integer
        email 2 : string

        .PhoneNumber {
            number 0 : string
            type 1 : integer
        }

        phone 3 : *PhoneNumber
    }

    .AddressBook {
        person 0 : *Person(id)
        others 1 : *Person
    }
    
parseReadBufferReadByte�    4   7  4 > 4   7  4 > 4   7  4 > 4   7  4 > 4  % > G  Unload Network...	warnDisconnectException
LoginConnectRemoveListener
Event� 	 " D4   % > 4   % > 4   % > 4   % > 5  4   % > 4   % > 4   % > 4  %	 >4  %
 >2  5 4 * ) 4 1 :4 1 :4 1 :4 1 :4 1 :4 1 :4 1 :4 1 :4 1 :4 1 :4 1! : 0  �G   Unload TestLoginSproto TestLoginPbc TestLoginPblua TestLoginBinary OnLogin OnDisconnect OnException OnConnect OnSocket 
StartNetwork3rd/sproto/print_rsproto.core3rd/sproto/sproto3rd/pbc/protobuf3rd/pblua/login_pb
EventeventsCommon/functionsCommon/protocalCommon/definerequire 