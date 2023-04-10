package com.cos.blog.test;

import java.util.List;
import java.util.function.Supplier;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.data.domain.Page;
import org.springframework.data.domain.Pageable;
import org.springframework.data.domain.Sort;
import org.springframework.data.web.PageableDefault;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PathVariable;
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
		
		
		// http://localhost:8000/blog/bummy/users
		@GetMapping("/dummy/users")
		public List<User> list() {
				// Json View 설치 : Chrome 확장프로그램에서 JsonView 설치
				// User테이블의 전체 리턴
				return userRepository.findAll();
		}
		
		
		// 한 페이지당 2건의 데이터를 리턴 받을것 ( Paging을 해보겠다 )
		// JPA에서 제공하는 Page 오브젝트
		// Paging 기본 전략 : 데이터는 2건씩, sort는 id로, 정렬은 id의 최신식으로 페이징처리 하겠다.
		// http://localhost:8000/blog/dummy/user?page=0  => [ 첫번째 페이지 호출 ( 제일 최신) ]
		// http://localhost:8000/blog/dummy/user?page=1   => [ 두번째 페이지 호출 ]
		@GetMapping("/dummy/user")
		public List<User> PagingList(@PageableDefault(size=2, sort = "id", direction = Sort.Direction.DESC) Pageable pageable) {
			// findAll ( Pageable )	
			// Page<T> findAll(Pageable pageable);
			Page<User> pagingUsers = userRepository.findAll(pageable);
			
			List<User> users = pagingUsers.getContent();
			return users;
		}
		
		// { id } 주소로 파라미터를 전달 받을 수 있습니다.
		// http://localhost:8000/blog/dummy/user/3
		// @PathVariable Url에 파라미터를 넣는다.
		@GetMapping("/dummy/user/{id}")
		public User detail(@PathVariable int id) {   
			
			// user/4번을 찾으면 내가 데이터베이스에서 못찾아오게 되면 user가 null이 될것 아니야?
			// 그럼 return 할때 null이 리턴이 되잖아 그러면 프로그램에 문제가 있지 않겠니?
			// 나는 Optional로 너의 User 객체를 감싸서 가져올테니 null인지 아닌지 판단해서 return해!!
			//userRepository.findById(id).get();			// get이라고 하면 User객체서 바로 뽑아서 리턴해
			User user = userRepository.findById(id).orElseThrow(new Supplier<IllegalArgumentException>() {
				@Override
				public IllegalArgumentException get() {
					// TODO Auto-generated method stub
					return new IllegalArgumentException(" Supplier<lllegalArgumentExeption>()의 해당 유저의 정보는 없습니다. id : " + id);
				}
			});
			
//			// 위 방식을 람다식 방법으로 처리 ( 람다식을 사용하면 리턴 타입 Supplier 타입을 리턴해야하는지 몰라도 된다 )
//			User user1 = userRepository.findById(id).orElseThrow(()-> {
//					return new IllegalArgumentException("람다식 표현 : 해당 유저의 정보는 없습니다. id : " + id);
//			});
//			return user1;
			
			
			// 요청 : 웹브라우저
			// User 객체 == 자바 오브젝트
			// 변환을 해야함 ( 웹 브라우저가 이해할 수 있는 데이터로 변환 => 가장 좋은게 JSON [Gson 라이브러리] )
			// 스프링부트 == MessageConverter라는 애가 응답시에 자동 작동
			// 만약에 자바 오브젝트를 리턴을하게 되면 MessageConverter가 Jackson 라이브러리를 호출해서
			// User 오브젝트를 json으로 변환해서 브라우저에게 던져 줍니다.
			return user;
		}
}
