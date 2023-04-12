package com.cos.blog.repository;

import org.springframework.data.jpa.repository.JpaRepository;

import com.cos.blog.model.User;

// 얘는 유저 테이블을 관리하는 Repository ( User테이블의 PK == Integer )
// 얘는  자동으로 빈으로 등록이 된다 그렇기 때문에 @Repository 어노테이션 생략 가능!
// 얘는 한마디로 치면 DAO ( DataBase Object ) 라고 보면 된다.
public interface UserRepository extends JpaRepository<User, Integer>{
		
		
}

// JpaRepository를 들어가서 찾아보면 
// findAll() 												    == 유저 데이터의 모든 데이터를 가져와라
// Iterable<T> findAll(Sort sort) 			== 유저테이블의 모든 데이터를 정렬을 해서 다 받을 수 있다.
// Page<T> findAll(Pageable pageable) == 유저 테이블의 모든 데이터를 Paging 처리해서 다 받을 수 있다.

// JpaRepository에서 더 올라가 보면 CrudRepository가 있는데 CrudRepository에는 save ( Insert, Update 동시 가능 )
//																											CrudRepository에는 findById ( Id의 값으로 레코드를 찾는것 )
//																											CrudRepository에는 deleteById ( Id로 값으로 레코드를 찾는 것 )


// JPA Naming 쿼리 전략
// 실제로 JPA가 들고 있지 않지만 이런 쿼리가 자동으로 실행이 된다.
// SELECT * FROM USER WHERE username = ?1 AND password = ?2
// SELECT * FROM USER WHERE username = username AND password = password
//User findByUsernameAndPassword(String username, String password);


//// nativeQuery를 이용한 login 메소드 
////  User를 리턴을 해준다.
//@Query(value = "SELECT * FROM user WHERE username = ?1 AND password = ?2", nativeQuery = true)
//User login(String username, String password);