package com.cos.blog.test;

import org.springframework.web.bind.annotation.DeleteMapping;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.PutMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.bind.annotation.RestController;

// 사용자가 요청을 했을 때 -> 응답(Data)   @RestController
// 사용자가 요청을 했을 때 -> 응답(Html)   @Controller

@RestController
public class HttpControllerTest {
	// 인터넷 브라우저 요청은 무조건 GET요청 밖에 할수가 없다.
	// http://localhost:8080/http/get	 ( SELECT )
	// 여러 데이터를 한번에 보내기 위해서 Member라는 객체를 매개변수로 받아와 getter로 호출을 할려고 할 때 
	// @requestParameter를 사용하면 안된다.
	@GetMapping("/http/get")
	public String getTest(Member mem) {
			return "get요청 : " + 
					"\n 유저 ID : " + mem.getId() + 
					"\n 유저이름 : " + mem.getuserName() + 
					"\n 유저비번 : " + mem.getPassword() +
					"\n 유저 이메일 : " + mem.getEmail();
	}
	
	// http://localhost:8080/http/post   ( INSERT )
	// Post방식은 Body라는 곳에 담아서 보낸다. form태그의 input에 요청하듯
	// Body 데이터는 반드시 @RequestBody를 사용해야 한다.
	// Json데이터로 보낼 시에는 변수의 타입을 맞춰서 보내야 한다.		
	// JSON의 MIME Type : application/json 
	//  MIME Type의 설정을 누가 하냐면 => 자동으로 MessageConvert ( 스프링부트 )
	@PostMapping("/http/post")
	public String postTest(@RequestBody Member mem) {
			return "post요청 : " + 
					"\n 유저 ID : " + mem.getId() + 
					"\n 유저이름 : " + mem.getuserName() + 
					"\n 유저비번 : " + mem.getPassword() +
					"\n 유저 이메일 : " + mem.getEmail();
	}
	
	// http://localhost:8080/http/put    ( UPDATE )
	@PutMapping("/http/put")
	public String putTest(@RequestBody Member mem) {
			return "post요청 : " + 
					"\n 유저 ID : " + mem.getId() + 
					"\n 유저이름 : " + mem.getuserName() + 
					"\n 유저비번 : " + mem.getPassword() +
					"\n 유저 이메일 : " + mem.getEmail();
	}
	
	//http://localhost:8080/delete		    ( DELETE )
	@DeleteMapping("/http/delete")
	public String deleteTest() {
			return "delete요청";
	}
}
