create database APIMitraCerdas
go

use APIMitraCerdas
go


create table Tasks(
	Id uniqueidentifier  not null Primary key,
	Nama varchar(30) null,
	Tugas varchar(100) not null,
	Deskripsi varchar(255) null,
	TanggalDeadline datetime
)

create table Users(
	id uniqueidentifier not null Primary key,
	username varchar(20) not null,
	passwords varchar(150) not null
)


