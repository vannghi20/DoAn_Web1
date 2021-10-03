Create database OnlineShop
use OnlineShop

--User--
Create table Account
(
	UserName nvarchar(50),
	Password nvarchar(10)
)

--Proc--
go
Create proc NewAccount
	@UserName nvarchar(50),
	@Password nvarchar(10)
	as
	begin
	declare @count int
	declare @res bit
	select @count = COUNT(*) from Account where @UserName = UserName and @Password = Password
	if(@count >0)
		set @res = 1
	else
		set @res = 0

	select @res
	end