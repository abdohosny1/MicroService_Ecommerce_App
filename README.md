# MicroServiceEcommerce

run docker compose ===>
docker-compose -f docker-compose.yml -f docker-compose.override.yml up -d

stop docker compose ===>
docker-compose -f docker-compose.yml -f docker-compose.override.yml down
docker run -d --hostname my-rabbit --name some-rabbit -p 15672:15672 -p 5672:5672 rabbitmq:3-management


link pgadmin ===> http://localhost:5050/login?next=%2F

link portainer  ===>http://localhost:9000/#!/auth


SELECT sname,grade,cname,
	   Prod_prev=lAG(grade) OVER(partition by Cname ORDER BY grade),
	   Prod_Next=LEAD(grade) OVER(partition by Cname ORDER BY grade)
FROM grades


![1](https://user-images.githubusercontent.com/43721664/180243035-9ad9648b-8806-48db-b218-49ef9417c5c1.PNG)
![2](https://user-images.githubusercontent.com/43721664/180243048-4546c267-118f-4fd9-80ef-704c681c402f.PNG)
![3](https://user-images.githubusercontent.com/43721664/180243057-ad01ff88-2ef0-4d8f-b8f3-fcf185c2297c.PNG)


