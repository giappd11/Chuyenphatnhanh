create table NguoiDung(
	ID int identity primary key,
	Ten varchar(50),
	DiaChi varchar(70),
	DienThoai varchar (17),
	
)

create table KhachHang(
	ID int identity primary key,
	TenKH varchar(50),
	DienThoai varchar(17),
	DiaChi varchar(70)
)

create table 