# MicroServiceEcommerce

run docker compose ===>
docker-compose -f docker-compose.yml -f docker-compose.override.yml up -d

stop docker compose ===>
docker-compose -f docker-compose.yml -f docker-compose.override.yml down
docker run -d --hostname my-rabbit --name some-rabbit -p 15672:15672 -p 5672:5672 rabbitmq:3-management


link pgadmin ===> http://localhost:5050/login?next=%2F

link portainer  ===>http://localhost:9000/#!/auth




-- create temp table

CREATE TABLE #EmpDetailss (
EmployeeId nvarchar(20), 
name nvarchar(100),
VacationName nvarchar(100),
FromDate varchar(20),
ToDate varchar(20),
Taken int ,
sumTotal int
);
--select  from temp table
select * from #EmpDetailss

--drop temp table
drop table #EmpDetailss

DECLARE @EmployeeId nvarchar(20) ,@namef nvarchar(100),
        @namev nvarchar(100),  @start date, @end date,
	    @empId nvarchar(20) ,@takenVacation int =0 ,@taken int  ;

DECLARE cursor_vacation CURSOR  FOR
SELECT ea.EmployeeId ,p.Name,
      v.VacationName  ,ev.FromDate,
	  ev.ToDate,
	  case when ev.ToDate>=ev.FromDate
	      then  DATEDIFF(day,ev.FromDate,ev.ToDate)+1
	      else  DATEDIFF(day,ev.FromDate,ev.ToDate) 
      end  
	 
 FROM   EmpAssignment ea  inner join Profile p on ea.EmpId=p.ProfileId 
        inner join  EmpVacat ev on ea.EmpId=ev.EmpId
		inner join  Vacation v on v.vaccode=ev.VacCode
		inner join  SSDocumentStatus  ss on ss.SSDocStatusNo=ev.VacStatus
         where ea.Status=1 and ss.SSDocStatusNo=1 
		order by p.Name,ev.FromDate

   OPEN cursor_vacation
   FETCH NEXT FROM cursor_vacation INTO @EmployeeId,@namef,@namev,@start,@end,@taken
       set @empId=@EmployeeId
	   if(@takenVacation=0)
	    begin
	     set @takenVacation=@taken
	    end
 WHILE @@FETCH_STATUS = 0  
    BEGIN 
	  
	INSERT INTO #EmpDetailss VALUES (
	@EmployeeId,
	@namef,
	@namev,
	FORMAT (@start, 'dd-MM-yy'),
    FORMAT (@end, 'dd-MM-yy'),
	@taken,@takenVacation )
	FETCH NEXT FROM cursor_vacation INTO @EmployeeId,@namef,@namev,@start,@end,@taken

	if  @empId =@EmployeeId
	  begin
	       set  @takenVacation=@takenVacation+@taken
	  end
   else 
	  begin
	      set @empId=@EmployeeId
		  set  @takenVacation=@taken
	  end	 
  END 
CLOSE cursor_vacation
DEALLOCATE cursor_vacation







		---test case ------


		SELECT ea.EmployeeId ,p.Name,
      v.VacationName  ,ev.FromDate,
	  ev.ToDate, 
	  case when ev.ToDate>=ev.FromDate
	      then  DATEDIFF(day,ev.FromDate,ev.ToDate)+1
	      else  DATEDIFF(day,ev.FromDate,ev.ToDate) 
     end
 FROM   EmpAssignment ea  inner join Profile p on ea.EmpId=p.ProfileId 
        inner join  EmpVacat ev on ea.EmpId=ev.EmpId
		inner join  Vacation v on v.vaccode=ev.VacCode
		inner join  SSDocumentStatus  ss on ss.SSDocStatusNo=ev.VacStatus
         where ea.Status=1 and ss.SSDocStatusNo=1
		order by ea.EmployeeId


select top 3 * from 	EmpAssignment
select top 3 * from 	Profile
select top 3 * from 	EmpVacat
select top 3 * from 	Vacation
select top 3 * from 	SSDocumentStatus


		select * 
		from EmpAssignment 
		where EmpAssignment.EmployeeId='2033'   

		select count(*)
		from EmpVacat 
		where EmpVacat.EmpId='216659' and EmpVacat.VacStatus=1  


		-- emp  EmployeeId = 'Z06546511'  EmpId='205951' take 26 vacation
		-- emp  EmployeeId = '2034'  EmpId='216657' take 6 vacation
		-- emp  EmployeeId = '2032'  EmpId='216658' take 4 vacation
		-- emp  EmployeeId = '2033'  EmpId='216659' take 5 vacation
		
