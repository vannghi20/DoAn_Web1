Create database FoodOder
go
use FoodOder

--User--
go
Create table FoodItem
(
	Id int IDENTITY(1,1) PRIMARY KEY,
    ImgSource nvarchar(255) NOT NULL,
    Title nvarchar(255),
    Descr nvarchar(500),
	Version int,
)
go
Insert into FoodItem values (1,'https://d1ralsognjng37.cloudfront.net/9dd6494b-84f6-4459-8b55-359d45f8723c.jpeg','Wet Burrito','French Fries, Any Kind of meat, Lettuce, Cheese, Sour Cream, Beans, Pico de Gallo, Avocado',0)
Insert into FoodItem values (2,'https://d1ralsognjng37.cloudfront.net/e05d185a-cca5-418e-b12d-4896e0e76100.jpeg','Super Burrito de Carne','Rice, Beans, Avocado, Sour Cream, Cheese, Hot Salsa, Onions and Choice of Meat.',0)
Insert into FoodItem values (3,'https://d1ralsognjng37.cloudfront.net/8271ecd9-5999-45ef-94bd-9548e20e2cc8','Shrimp Dumplings','French Fries, Any Kind of meat, Lettuce, Cheese, Sour Cream, Beans, Pico de Gallo, Avocado',0)
--Proc Select All FoodItem--
go
Create proc SelectFoodItem
	as
	Select * from FoodItem

--Proc Select FoodItem by Id--
go
Create proc SelectFoodItemById
	@Id int
	as
	Select * from FoodItem where Id = @Id