package com.cos.blog.service;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;

import com.cos.blog.model.User;
import com.cos.blog.repository.UserRepository;

// 스프링이 컴포넌트 스캔을 통해서 빈에 등록을 해준다.  ( IoC를 해준다 ) 메모리를 대신 띄어준다.
@Service
public class UserService {
	
		@Autowired
		private UserRepository userRepository;
		
		// javax의 @Transaction
		// 회원가입 함수의 전체가 하나의 트랜잭션이 된다. 전체가 실행이되면 그때 커밋이 될꺼구요
		// 트랜잭션중 하나라도 실패를 한다면 커밋이 되지 않고 RollBack이 될겁니다.
		@Transactional
		public void 회원가입(User user) {
			// userRepository 하나의 트랜잭션
			userRepository.save(user);
		}
		
		// SELECT할 때 트랜잭션이 실행이 된다.
		// 해당 서비스가 종료될 때 트랜잭션가 종료될텐데 이때까지는 데이터의 정합성을 유지할 수 있음
		@Transactional(readOnly = true)
		public User 로그인(User user) {
			return user = userRepository.findByUsernameAndPassword(user.getUsername(), user.getPassword());
		}
}