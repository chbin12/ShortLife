LJ�   H4   % > 4  % >4  % >7 % >77> 7%	 >4
 % >  >4
 % >3 2 3 2 3	 ;	3	 ;	:''93 2 3	 ;	:' N9:2 3 2 3	 ;	:;:4 % > 7% 	 > 7%	 
 > 	 >G  decodeAddressBookencode	stopcollectgarbageothers number9876543210 id��	name
Carolperson   	typenumber01234567890 id��	nameBob
phone 	typenumber87654321 	typenumber123456789 id�N	name
Alice--------------default table for Person
printPersondefault__cobjdumpproto�.Person {
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

parseprint_rsproto.coresprotorequire 