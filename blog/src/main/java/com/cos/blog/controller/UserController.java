package com.cos.blog.controller;

import java.util.UUID;

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

import com.cos.blog.model.KakaoProfile;
import com.cos.blog.model.OAuthToken;
import com.fasterxml.jackson.core.JsonProcessingException;
import com.fasterxml.jackson.databind.JsonMappingException;
import com.fasterxml.jackson.databind.ObjectMapper;

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
		params.add("grant_type", "authorization_code");
		params.add("client_id", "3aaf6213852724e22060920d2d73c05b");
		params.add("redirect_uri", "http://localhost:8000/auth/kakao/callback");
		params.add("code", code);

		// HttpHeader와 HttpBody를 하나의 오브젝트에 담기 ( 이유 : rt.exchange 함수가 HttpEntity 오브젝트를 넣게
		// 되어있어서 )
		HttpEntity<MultiValueMap<String, String>> kakaoTokenRequest = new HttpEntity<>(params, headers);

		// Http 요청하기 - Post 방식으로 - 그리고 response 변수의 응답 받음.
		ResponseEntity<String> response = rt.exchange("https://kauth.kakao.com/oauth/token", HttpMethod.POST,
				kakaoTokenRequest, String.class);

		// Gson, Json Simple, ObjectMapper 라이브러리 있음
		ObjectMapper objectMapper = new ObjectMapper();

		OAuthToken outhAuthToken = null;

		// OAuthToken에 response 데이터 저장
		try {
			// JSON 데이터를 자바 오브젝트로 변경
			outhAuthToken = objectMapper.readValue(response.getBody(), OAuthToken.class);
		} catch (JsonMappingException e) {
			e.printStackTrace();
		} catch (JsonProcessingException e) {
			e.printStackTrace();
		}

		// 카카오 AccessToken 확인
		System.out.println("카카오 액세스 토큰 : " + outhAuthToken.getAccess_token());

		// 토큰을 통한 사용자 조회
		RestTemplate rt2 = new RestTemplate();

		// HttpHeader 오브젝트 생성 ( Header의 정보를 담을 Headers )
		HttpHeaders headers2 = new HttpHeaders();
		// Bearer 앞에 한칸 띄어 놓아야 합니다.
		headers2.add("Authorization", "Bearer " + outhAuthToken.getAccess_token());
		headers2.add("Content-type", "application/x-www-form-urlencoded;charset=utf-8");

		// HttpHeader와 HttpBody를 하나의 오브젝트에 담기 ( 이유 : rt.exchange 함수가 HttpEntity 오브젝트를 넣게
		// 되어있어서 )
		HttpEntity<MultiValueMap<String, String>> kakaoProfileRequest2 = new HttpEntity<>(headers2);

		// Http 요청하기 - Post 방식으로 - 그리고 response 변수의 응답 받음.
		ResponseEntity<String> response2 = rt2.exchange("https://kapi.kakao.com/v2/user/me", HttpMethod.POST,
				kakaoProfileRequest2, String.class);
		// 카카오 토큰 요청 완료 : 토큰 요청에 대한 응답
		System.out.println(response2.getBody());
		
		ObjectMapper objectMapper2 = new ObjectMapper();
		KakaoProfile kakaoProfile = null;
		
		try {
			kakaoProfile = objectMapper2.readValue(response2.getBody(), KakaoProfile.class);
		}catch (JsonMappingException e) {
			e.printStackTrace();
		}catch (JsonProcessingException e) {
			e.printStackTrace();
		}
		
		
		// User 오브젝트 : username, password, email
		System.out.println("카카오 프로파일        : " + kakaoProfile);
		System.out.println("카카오 아이디(번호) : " + kakaoProfile.getId());
		System.out.println("카카오 이메일           : " + kakaoProfile.getKakao_account().getEmail());
		
		System.out.println("블로그 서버 유저네임 : " + kakaoProfile.getKakao_account().getEmail() + "-" + kakaoProfile.getId());
		System.out.println("블로그 서버 이메일    : " +kakaoProfile.getKakao_account().getEmail());
		
		// 임시 패스워드 만들기위한 UUID 객체 사용 
		UUID garbagePassword = UUID.randomUUID();
		System.out.println("블로그 서버 패스워드 : " + garbagePassword);
		
		return response2.getBody();
	}
}
