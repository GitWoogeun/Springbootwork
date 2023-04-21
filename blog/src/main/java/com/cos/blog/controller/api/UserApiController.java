package com.cos.blog.controller.api;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.security.authentication.AuthenticationManager;
import org.springframework.security.authentication.UsernamePasswordAuthenticationToken;
import org.springframework.security.core.Authentication;
import org.springframework.security.core.context.SecurityContextHolder;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.PutMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RestController;

import com.cos.blog.dto.ResponseDto;
import com.cos.blog.model.User;
import com.cos.blog.service.UserService;

// 얘는 나중에 웹에도 쓸수 있다.
//데이터만 리턴을 해주는 컨트롤러 이기 때문에 @RestController
@RestController						
public class UserApiController {

		// DI ( Spring이 컴포넌트 스캔할 때 스프링 빈을 통해서 IoC에 띄어준다 )
		@Autowired
		private UserService userService;

		@Autowired
		private AuthenticationManager authenticationManager;

		
		// JSON이니까 @RequestBody로 파라미터 받음
		// 통신상태를 확인하기 위해 HttpStatus.OK 
		// username, password, email
		@PostMapping("/auth/joinProc")
		public ResponseDto<Integer> save(@RequestBody User user) {

			System.out.println("UserApiController : save 호출됨!"); 
			
			// 실제로 여기서 DB에 insert를 하고 아래에서 return이 되면 되요.
			userService.회원가입(user);
			
			// 회원가입이 성공 시 [ 상태값 : 200, 1 ] 호출
			// 자바 오브젝트를 JSON으로 변환해서 리턴 (Jackson)
			return new ResponseDto<Integer>(HttpStatus.OK.value(), 1);
		}
	
		// 유저의 정상적인 통신이 된다면 상태값 200(성공)과 1을 리턴
		// key = value, x-www-form-urlencoded
		// @RequestBody : HTTP 요청 본문에 있는 데이터를 자동으로 자바의 객체로 변환 하는데 사용
		@PutMapping("/user")
		public ResponseDto<Integer> update(@RequestBody User user)
		{		
			userService.회원수정(user);
			
			// 트랜잭션이 종료되기 때문에 DB의 값은 변경이 되었지만,
			// 하지만 Session 값은 변경되지 않은 상태이기 때문에 우리가 직접 Session값을 변경 해줄 것임.
			// Session 등록
			Authentication authentication = authenticationManager
					.authenticate(new UsernamePasswordAuthenticationToken(user.getUsername(), user.getPassword()));
			SecurityContextHolder.getContext().setAuthentication(authentication);
			
			return new ResponseDto<Integer>(HttpStatus.OK.value(), 1);
		}
}


//// 전통적인 로그인 방식
//// 다음시간에 스프링 시큐리티 이용해서 로그인!!
//@PostMapping("/api/user/login")
//public ResponseDto<Integer> login(@RequestBody User user, HttpSession session) {
//	System.out.println("UserApiController : login 호출됨");
//	User principal = userService.로그인(user);		// Printcipal = 정보주체의
//	// 유저 로그인 정보가 null이 아니라면
//	if( principal != null) {
//		// 이렇게 하면 session이 만들어집니다.
//		session.setAttribute("principal", principal);
//	}
//	return new ResponseDto<Integer>(HttpStatus.OK.value(), 1);			
//}