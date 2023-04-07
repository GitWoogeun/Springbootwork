package com.cos.blog.test;

import org.springframework.web.bind.annotation.DeleteMapping;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.PutMapping;
import org.springframework.web.bind.annotation.RestController;

// 사용자가 요청을 했을 때 -> 응답(Data)   @RestController
// 사용자가 요청을 했을 때 -> 응답(Html)   @Controller

@RestController
public class HttpControllerTest {

	// 인터넷 브라우저 요청은 무조건 GET요청 밖에 할수가 없다.
	// http://localhost:8080/http/get	 ( SELECT )
	@GetMapping("/http/get")
	public String getTest() {
			return "get요청";
	}
	// http://localhost:8080/http/post   ( INSERT )
	@PostMapping("/http/post")
	public String postTest() {
			return "post요청";
	}
	// http://localhost:8080/http/put    ( UPDATE )
	@PutMapping("/http/put")
	public String putTest() {
			return "put요청";
	}
	//http://localhost:8080/delete		    ( DELETE )
	@DeleteMapping("/http/delete")
	public String deleteTest() {
			return "delete요청";
	}
}
