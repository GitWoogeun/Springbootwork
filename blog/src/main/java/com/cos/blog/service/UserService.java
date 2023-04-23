package com.cos.blog.service;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.security.crypto.bcrypt.BCryptPasswordEncoder;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;

import com.cos.blog.model.User;
import com.cos.blog.repository.UserRepository;

// 스프링이 컴포넌트 스캔을 통해서 빈에 등록을 해준다.  ( IoC를 해준다 ) 메모리를 대신 띄어준다.
@Service
public class UserService {
	
		// JPA방식의 데이터베이스 접근
		@Autowired
		private UserRepository userRepository;
		
		// Password 암호화를 하기 위한 객체 생성
		@Autowired
		private BCryptPasswordEncoder encoder;
	
		
		// javax의 @Transaction
		// 회원가입 함수의 전체가 하나의 트랜잭션이 된다. 전체가 실행이되면 그때 커밋이 될꺼구요
		// 트랜잭션중 하나라도 실패를 한다면 커밋이 되지 않고 RollBack이 될겁니다.
		@Transactional
		public void 회원가입(User user) {
			String rawPassword = user.getPassword();   						// 원문 비밀번호
			String encPassword = encoder.encode(rawPassword);		// 암호화 비밀번호 ( 해쉬 처리가 됨 )			
			user.setPassword(encPassword);											// 비밀번호 암호화 적용된 데이터 Password에 셋팅			
			userRepository.save(user);														// userRepository 하나의 트랜잭션
		}
		
		@Transactional
		public void 회원수정(User user) {
			// 수정 시 영속성 컨텍스트에 User 오브젝트를 영속화를 시키고, 영속화된 User 오브젝트를 수정
			// SELECT를 해서 User 오브젝트를 DB로 부터 가져오는 이유는 영속화를 하기 위해서!!
			// 영속화된 오브젝트를 변경하면 자동으로 DB에 updat문을 날려주거든요.
			User persistence = userRepository.findById(user.getId())
					.orElseThrow(()-> {
							return new IllegalArgumentException("회원 찾기 실패");
					});
			String rawPassword = user.getPassword();
			String encPassword = encoder.encode(rawPassword);

			// 회원 수정 함수 종료 시 = 서비스 종료 = 트랜잭션 종료 = 커밋이 자동으로 됩니다.
			// 영속화된 persistence 객체의 변화가 감지되면 더티체킹이 되어 update문을 날려줌.
			persistence.setPassword(encPassword);
			persistence.setEmail(user.getEmail());
		}
}