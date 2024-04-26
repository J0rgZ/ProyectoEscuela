create database AppColegio
go

use AppColegio
go

--Tabla Tipo--
create table Tipo(
id_tipo char(5) primary key,
tipo_nombre varchar(50),
)

--Datos Tipo--
insert into Tipo values('T0001','Administrador')
insert into Tipo values('T0002','Asistente')
insert into Tipo values('T0003','Secretaria')

go

--Tabla Usuarios--
create table Usuario(
id_codigo char(5) primary key,
nombre varchar(50),
usuario varchar(10),
contraseña varchar(10),
id_tipo char(5),
FOREIGN KEY (id_tipo) REFERENCES Tipo (id_tipo)
);

--Datos Usuarios--
insert into Usuario values('U0001','Carlos Admin','adm','adm','T0001')
insert into Usuario values('U0002','Alfreds Maria','alf1','123','T0002')
insert into Usuario values('U0003','Davolio Nancy','dav1','123','T0002')
insert into Usuario values('U0004','Ana Andrew','ful1','123','T0003')
insert into Usuario values('U0005','Leverling Janet','lev1','123','T0002')
insert into Usuario values('U0006','Peacock Margaret','pea1','123','T0003')
insert into Usuario values('U0007','Suyama	Michael','suy1','123','T0003')
insert into Usuario values('U0008','King Alejandro','king1','123','T0001')
insert into Usuario values('U0009','Callahan Laura','cal1','123','T0002')
insert into Usuario values('U0010','Eren Jaeger','eren1','123','T0001')
insert into Usuario values('U0011','El kevin','kev1','123','T0002')
insert into Usuario values('U0012','Ana Sleider','Ani1','123','T0003')
go

--Tabla Salones--
create table Salones(
id_salon char(5) primary key,
salon_nombre varchar(50),
);

insert into Salones values('S0001','A401')
insert into Salones values('S0002','A402')
insert into Salones values('S0003','B301')
insert into Salones values('S0004','B302')
insert into Salones values('S0005','C345')
insert into Salones values('S0006','C346')
insert into Salones values('S0007','D201')
insert into Salones values('S0008','D320')
insert into Salones values('S0009','F321')
insert into Salones values('S0010','F322')
insert into Salones values('S0011','G101')
insert into Salones values('S0012','G102')

create table Cursos(
id_cursos char(5) primary key,
curso_nombre varchar(50),
);

insert into Cursos values('B0001','Programación')
insert into Cursos values('B0002','Diseño')
insert into Cursos values('B0003','Ciencias')
insert into Cursos values('B0004','Historia')
insert into Cursos values('B0005','Comunicación')
insert into Cursos values('B0006','Matemáticas')

create table Alumnos(
id_alumno char(5) primary key,
nombre varchar(50),
telefono int,
matricula int,
id_cursos char(5),
id_salon char(5),
FOREIGN KEY (id_cursos) REFERENCES Cursos (id_cursos),
FOREIGN KEY (id_salon) REFERENCES Salones (id_salon)
);

insert into Alumnos values('A0001','Maria Anders','123456789','123456','B0001','S0001')
insert into Alumnos values('A0002','Guillermo Fernández','452145210','412587','B0001','S0001')
insert into Alumnos values('A0003','Georg Pipps','120155232','854721','B0001','S0002')
insert into Alumnos values('A0004','Bernardo Batista','658201423','456321','B0001','S0001')
insert into Alumnos values('A0005','Jonas Bergulfsen','012365421','321456','B0005','S0010')
insert into Alumnos values('A0006','Art Braunschweiger','325742120','987456','B0005','S0009')
insert into Alumnos values('A0007','Liz Nixon','698524630','654789','B0005','S0010')
insert into Alumnos values('A0008','Helvetius Nagy','123001452','459314','B0003','S0005')
insert into Alumnos values('A0009','Paula Parente','965200010','684236','B0003','S0006')
insert into Alumnos values('A0010','Zbyszek Piestrzeniewicz','524558200','948526','B0004','S0008')
go

-----------------------------------------------------------------------------------------------------------------------------------------
--Procedimiento almacenado buscar usuarios--
create proc sp_buscar_usuario
@nombre varchar(50)
as
select id_codigo,u.nombre,usuario,t.id_tipo,tipo_nombre as Tipo 
from Usuario u, Tipo t where t.id_tipo=u.id_tipo and nombre like '%' + @nombre + '%'
go

--Procedimiento almacenado listar usuarios--
create proc  sp_listar_usuarios
as
select id_codigo,u.nombre,usuario,t.id_tipo,tipo_nombre as Tipo 
from Usuario u inner join Tipo t on u.id_tipo = t.id_tipo
order by id_codigo
go


--Procedimiento almacenado iniciar sesión--
create proc sp_logueo
@usuario varchar(10),
@contraseña varchar(10)
as
select nombre,usuario,contraseña,id_tipo,id_codigo from Usuario 
where usuario=@usuario and contraseña=@contraseña
go

--Listar tipo--
create proc sp_listar_tipo
as
select id_tipo,tipo_nombre from Tipo
go

--Mantenimiento de usuarios--
create proc sp_mantenimiento_usuario
@id_codigo char(5),
@nombre varchar(50),
@usuario varchar(10),
@id_tipo char(5),
@accion varchar(50) output
as
if(@accion='1')
  begin
    declare @codnew varchar(5),@codmax varchar(5)
    set @codmax=(select MAX(id_codigo) from Usuario)
    set @codmax=isnull(@codmax,'U0000')
    set @codnew='U'+ RIGHT(RIGHT(@codmax,4)+10001,4)

	if((select count(*) from Usuario where usuario = @usuario) > 0)
	begin
		SET @accion='El usuario "' + @usuario + '" ya existe'
	end
	else if((select count(*) from Usuario where nombre = @nombre) > 0)
	begin
		SET @accion= @nombre + ' ya se encuentra registrad@'
	end
	else 
	begin
		INSERT INTO Usuario(id_codigo,nombre,usuario,contraseña,id_tipo)
		values(@codnew,@nombre,@usuario,@usuario,@id_tipo)
		SET @accion='Se Genero el código '+@codnew
	end
  end
ELSE IF(@accion='2')
  BEGIN
    UPDATE Usuario SET nombre=@nombre,usuario=@usuario,id_tipo=@id_tipo where id_codigo=@id_codigo    
	SET @accion='Se Actualizo el código: ' + @id_codigo
  END  
  else if (@accion='3')
  begin
  delete from Usuario where id_codigo=@id_codigo
    SET @accion='Se borro el código: ' + @id_codigo
  end
  go

--Procedimiento almacenado listar salon--
create proc  sp_listar_salon
as
select * from Salones
go

--Mantenimiento de salon
create proc sp_mantenimiento_salon
@id_salon char(5),
@salon_nombre varchar(50),
@accion varchar(250) output
as
if(@accion='1')
  begin
    declare @codnew varchar(5),@codmax varchar(5)
    set @codmax=(select MAX(id_salon) from Salones)
    set @codmax=isnull(@codmax,'S0000')
    set @codnew='S'+ RIGHT(RIGHT(@codmax,4)+10001,4)
	if((select count(*) from Salones where salon_nombre = @salon_nombre) > 0)
	begin
		SET @accion='El nombre del salón "' + @salon_nombre + '" ya existe'
	end
	else
	begin
		INSERT INTO Salones(id_salon,salon_nombre)
		values(@codnew,@salon_nombre)
		SET @accion='Se Genero el código '+@codnew
	end
  end
ELSE IF(@accion='2')
  BEGIN
	UPDATE Salones SET salon_nombre=@salon_nombre where id_salon=@id_salon 
	SET @accion='Se Actualizo el código: ' + @id_salon
  END  
  else if (@accion='3')
  begin
	if ((select count(*) from Alumnos a inner join Salones s on a.id_salon = s.id_salon 
	where a.id_salon = @id_salon) > 0)
	begin
		SET @accion='No puede eliminar el salon "' + @id_salon + '"'
	end
	else
	begin
		delete from Salones where id_salon=@id_salon 
		SET @accion='Se borro el código: ' + @id_salon
	end
  end
  go


--Procedimiento almacenado listar cursos--
create proc  sp_listar_cursos
as
select * from Cursos
go
--Mantenimiento de cursos
create proc sp_mantenimiento_cursos
@id_cursos char(5),
@curso_nombre varchar(50),
@accion varchar(250) output
as
if(@accion='1')
  begin
    declare @codnew varchar(5),@codmax varchar(5)
    set @codmax=(select MAX(id_cursos) from Cursos)
    set @codmax=isnull(@codmax,'B0000')
    set @codnew='B'+ RIGHT(RIGHT(@codmax,4)+10001,4)

	if((select count(*) from Cursos where curso_nombre = @curso_nombre) > 0)
	begin
		SET @accion='El nombre del curso "' + @curso_nombre + '" ya existe'
	end
	else
	begin
		INSERT INTO Cursos(id_cursos,curso_nombre)
		values(@codnew,@curso_nombre)
		SET @accion='Se Genero el código '+@codnew
	end
  end
ELSE IF(@accion='2')
  BEGIN
    UPDATE Cursos SET curso_nombre=@curso_nombre where id_cursos=@id_cursos
	SET @accion='Se Actualizo el código: ' + @id_cursos
  END  
  else if (@accion='3')
  begin
	if ((select count(*) from Alumnos a inner join Cursos c on a.id_cursos = c.id_cursos 
	where a.id_cursos = @id_cursos) > 0)
	begin
		SET @accion='No puede eliminar el curso "' + @id_cursos + '"'
	end
	else
	begin
		delete from Cursos where id_cursos=@id_cursos 
		SET @accion='Se borro el código: ' + @id_cursos
	end
  end
  go

  --Procedimiento almacenado cambiar constraseña--
create proc  sp_pass
@id_codigo char(5),
@contraseña varchar(50),
@accion varchar(50) output
as
if(@accion='1')
  begin
update Usuario set contraseña=@contraseña where id_codigo=@id_codigo
SET @accion='Se actualizo la contraseña del usuario: ' + @id_codigo
  END  
go

  --Procedimiento almacenado contar cursos
 create proc sp_total_cursos
 as
 select count(id_cursos) as cursos from Cursos
 go

   --Procedimiento almacenado contar salon
 create proc sp_total_salon
 as
 select count(id_salon) as salones from Salones
 go

   --Procedimiento almacenado contar alumnos
 create proc sp_total_alumnos
 as
 select count(id_alumno) as alumnos from Alumnos
 go


  --Procedimiento almacenado buscar alumnos--
create proc  sp_buscar_alumnos
@nombre varchar(50)
as
select id_alumno,nombre,telefono,matricula,c.id_cursos,curso_nombre,s.id_salon,salon_nombre 
from Alumnos a 
inner join Cursos c on a.id_cursos = c.id_cursos 
inner join Salones s on a.id_salon=s.id_salon
where nombre like '%' + @nombre + '%'
order by id_alumno
go

 --Procedimiento almacenado listar alumnos--
create proc  sp_listar_alumnos
as
select id_alumno,nombre,telefono,matricula,c.id_cursos,curso_nombre,s.id_salon,salon_nombre 
from Alumnos a
inner join Cursos c on a.id_cursos = c.id_cursos 
inner join Salones s on a.id_salon=s.id_salon
order by id_alumno
go

--Mantenimiento de alumnos
create proc sp_mantenimiento_alumnos
@id_alumno char(5),
@nombre varchar(50),
@telefono int,
@matricula int,
@id_curso char(5),
@id_salon char(5),
@accion varchar(50) output
as
if(@accion='1')
  begin
    declare @codnew varchar(5),@codmax varchar(5)
    set @codmax=(select MAX(id_alumno) from Alumnos)
    set @codmax=isnull(@codmax,'A0000')
    set @codnew='A'+ RIGHT(RIGHT(@codmax,4)+10001,4)
	if((select count(*) from Alumnos where nombre = @nombre) > 0)
	begin
		SET @accion= @nombre + ' ya se encuentra registrad@'
	end
	else
	begin
	    INSERT INTO Alumnos(id_alumno,nombre,telefono,matricula,id_cursos,id_salon)
		values(@codnew,@nombre,@telefono,@matricula,@id_curso,@id_salon)
		SET @accion='Se Genero el código '+@codnew
	end
  end
ELSE IF(@accion='2')
  BEGIN
    UPDATE Alumnos SET nombre=@nombre,telefono=@telefono,matricula=@matricula,id_cursos=@id_curso,id_salon=@id_salon where id_alumno=@id_alumno
	SET @accion='Se Actualizo el código: ' + @id_alumno
  END  
  else if (@accion='3')
  begin
  delete from Alumnos where id_alumno=@id_alumno
    SET @accion='Se borro el código: ' + @id_alumno
  end
  go

 create proc  sp_buscar_alumnos_curso
@id_curso char(5)
as
select id_alumno,nombre,telefono,matricula,c.id_cursos,curso_nombre,s.id_salon,salon_nombre 
from Alumnos a
inner join Cursos c on a.id_cursos = c.id_cursos 
inner join Salones s on a.id_salon=s.id_salon
where a.id_cursos=@id_curso order by id_alumno
go

 create proc  sp_buscar_alumnos_salon
@id_salon char(5)
as
select id_alumno,nombre,telefono,matricula,c.id_cursos,curso_nombre,s.id_salon,salon_nombre 
from Alumnos a
inner join Cursos c on a.id_cursos = c.id_cursos 
inner join Salones s on a.id_salon=s.id_salon
where a.id_salon= @id_salon order by id_alumno
go

Select * From Usuario
Select * From Alumnos
Select * From Cursos
Select * From Salones