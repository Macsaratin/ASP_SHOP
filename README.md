--asp_shop
--Chuyển đến thư mục dự án:

cd asp_shop

--Cài đặt các gói phụ thuộc:

dotnet restore

--Áp dụng các di trú cơ sở dữ liệu:

dotnet ef migrations remove 

dotnet ef migrations add InitialCreate

dotnet ef database update

dotnet run


--Chạy ứng dụng:

dotnet run


