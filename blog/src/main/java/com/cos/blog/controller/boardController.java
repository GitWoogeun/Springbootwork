package com.cos.blog.controller;

import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.GetMapping;

@Controller
public class boardController {
	// @AuthenticationPrincipal PrincipalDetail principal 로그인 인증처리할 때 필요 ( 메인페이지는 로그인을 안해도 되기 때문에 주석 )
	@GetMapping({"" , "/"})					
	public String index() {							// 컨트롤러에서 세션을 어떻게 찾는지..?
		
		//	 /WEB-INF/views/index.jsp
		return "index";
	}
	
	// USER 권한이 필요
	@GetMapping("/board/saveForm")
	public String saveForm() {
		return "board/saveForm";
	}
}
