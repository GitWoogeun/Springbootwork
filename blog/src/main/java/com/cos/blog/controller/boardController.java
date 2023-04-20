package com.cos.blog.controller;

import org.springframework.security.core.annotation.AuthenticationPrincipal;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.GetMapping;

import com.cos.blog.config.auth.PrincipalDetail;

@Controller
public class boardController {
	
	@GetMapping({"" , "/"})					
	public String index(@AuthenticationPrincipal PrincipalDetail principal) {							// 컨트롤러에서 세션을 어떻게 찾는지..?
		
		System.out.println("로그인 사용자 ID : " + principal.getUsername());
		
		//		/WEB-INF/views/index.jsp
		return "index";
	}
}
