--create table TicketEntry (TicketID int primary key identity,Header nvarchar(50),TicDescription nvarchar(50),TicStatus nvarchar(50),Username nvarchar(50));

select * from TicketEntry

Insert into TicketEntry(Header,TicDescription,TicStatus,Username) values('Hi','Wrong Ui','2','hi@gmail.com');