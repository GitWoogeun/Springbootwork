package com.cos.blog.controller;

import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.GetMapping;

// 인증이 안된 사용자들이 출입할 수 있는 경로를 /auth/** 경로 허용
// 그냥 주소가 / 이면 index.jsp 허용
// static 이하에 있는 /js/**. /css/**. /image/** 허용
@Controller
public class UserController {
	
	// 회원가입
	@GetMapping("/auth/joinForm")
	public String joinForm() {
		return "user/joinForm";
	}
	
	// 로그인 
	@GetMapping("/auth/loginForm")
	public String loginForm() {
		return "user/loginForm";
	}
	
	// 회원수정
	// 인증된 사용자의 정보는 Authentication 객체에 저장이 됩니다.
	// @AuthenticationPrincipal 어노테이션은 인증된 사용자의 정보를 주입할 때 사용하는 어노테이션
	// @AuthenticationPrincipal 어노테이션은 이 Authentication 객체에서 사용자 정보를 추출하고,
	// @AuthenticationPrincipal PrincipalDetail principal
	// 해당 정보를 Controller의 매개변수에 주입 합니다.
	@GetMapping("/user/updateForm")
	public String updateForm() {
		return "user/updateForm";
	}
}
