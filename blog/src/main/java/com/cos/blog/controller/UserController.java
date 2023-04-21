package com.cos.blog.controller;

import org.springframework.http.HttpEntity;
import org.springframework.http.HttpHeaders;
import org.springframework.http.HttpMethod;
import org.springframework.http.ResponseEntity;
import org.springframework.stereotype.Controller;
import org.springframework.util.LinkedMultiValueMap;
import org.springframework.util.MultiValueMap;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.ResponseBody;
import org.springframework.web.client.RestTemplate;

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
	
	// 카카오 로그인
	// @ResponseBody를 붙이면 데이터를 리턴해주는 함수가 되는거에요~
	@GetMapping("/auth/kakao/callback")
	public @ResponseBody String kakaoCallback(String code) {
			
			// POST 방식으로 key = value 타입의 데이터를 요청 ( 카카오 쪽으로 )
			// Retrofit2
			// OkHttp
			// RestTemplate
		
			RestTemplate rt = new RestTemplate();
			
			// HttpHeader 오브젝트 생성 ( Header의 정보를 담을 Headers )
			HttpHeaders headers = new HttpHeaders();
			headers.add("Content-type", "application/x-www-form-urlencoded;charset=utf-8");
			
			// HttpBody 오브젝트 생성 ( Body의 정보를 담을 MultiValueMap )
			MultiValueMap<String, String> params = new LinkedMultiValueMap<>();
			params.add("grant_type"  , "authorization_code");
			params.add("client_id"        , "3aaf6213852724e22060920d2d73c05b");
			params.add("redirect_uri" , "http://localhost:8000/auth/kakao/callback");
			params.add("code"               , code);
			
			// HttpHeader와 HttpBody를 하나의 오브젝트에 담기 ( 이유 : rt.exchange 함수가 HttpEntity 오브젝트를 넣게 되어있어서 )
			HttpEntity<MultiValueMap<String, String>> kakaoTokenRequest = 
						new HttpEntity<>(params, headers);
			
			// Http 요청하기 - Post 방식으로 - 그리고 response 변수의 응답 받음.
			ResponseEntity<String> response = rt.exchange(
					"https://kauth.kakao.com/oauth/token",
					HttpMethod.POST,
					kakaoTokenRequest,
					String.class
			);
			
			return "카카오 토큰 요청 완료 : 토큰 요청에 대한 응답 = " + response;
	}
}
