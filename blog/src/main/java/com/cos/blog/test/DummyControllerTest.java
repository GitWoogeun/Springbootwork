package com.cos.blog.test;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RestController;

import com.cos.blog.model.RoleType;
import com.cos.blog.model.User;
import com.cos.blog.repository.UserRepository;

@RestController
public class DummyControllerTest {
		
		// DummyController가 메모리에 뜰떄 UserRepository 또한 같이 메모리에 뜬다. ( Spring이 Componunt할 떄 )
		@Autowired			// 의존성 주입 ( DI )
		private UserRepository userRepository;
	
		// @RequestParam("실제 DB의 컬럼명") String username 이렇게 @RequestParam 어노테이션을 사용한다면,
		// String username을 꼭 db의 컬럼명과 동일하게 적지 않아도 된다.
		
		// http://localhost:8000/blog/dummy/join (요청)
		// http의 body에 username, password, email 데이터를 가지고 (요청)하면
		// 또 다른 방법으로는 변수명을 실제 DB의 컬럼명과 동일하게 적는다면 @RequestParam 어노테이션을 사용하지 않아도 된다.
		@PostMapping("/dummy/join")		// => insert를 할꺼기 때문에 postMapping 사용
		public String join( User user) {		// ( 이렇게 하면 오브젝트 형태로 받을 수 있다. )key = value 형태를 받아준다. ( 약속된 규칙 )
				System.out.println("userId         : " + user.getId());							// Id는 auto-increament가 된다 ( 시퀀스 )
				System.out.println("username  : " + user.getUsername());
				System.out.println("password   : " + user.getPassword());
				System.out.println("email           : " + user.getEmail());
				System.out.println("role             : " + user.getRole());							// default값이 작동을 하려면 insert할 때 role컬럼을 빼서 insert 한다.
				System.out.println("createDate : " + user.getCreateDate());			// 자바에서 @CreationTimestamp로 현재시간을 구해서 데이터를 넣어줌
				
				// Enum을 사용하여 실수를 방지할수 있다. ( 내가 넣는 값을 강제 할 수 있습니다. USER 아니면 ADMIN만 넣을수 있다 )
				// RoleType USER라고 명시
				user.setRole(RoleType.USER);			
				// 회원가입의 정보를 DB에 넣어주는 SAVE Function 실행!
				userRepository.save(user);
				
				return "회원가입 성공!";
		}
}
