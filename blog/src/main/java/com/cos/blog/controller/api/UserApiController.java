package com.cos.blog.controller.api;

import org.springframework.http.HttpStatus;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RestController;

import com.cos.blog.dto.ResponseDto;
import com.cos.blog.model.User;

// 얘는 나중에 웹에도 쓸수 있다.
//데이터만 리턴을 해주는 컨트롤러 이기 때문에 @RestController
@RestController						
public class UserApiController {
	
		// JSON이니까 @RequestBody로 파라미터 받음
		// 통신상태를 확인하기 위해 HttpStatus.OK 
		@PostMapping("/api/user")
		public ResponseDto<Integer> save(@RequestBody User user) {
			System.out.println("UserApiController : save 호출됨!");
			
			// 실제로 여기서 DB에 insert를 하고 아래에서 return이 되면 되요.
			
			
			// 회원가입이 성공 시 [ 상태값 : 200, 1 ] 호출
			return new ResponseDto<Integer>(HttpStatus.OK, 1);	// 자바 오브젝트를 JSON으로 변환해서 리턴 (Jackson)
		}
}