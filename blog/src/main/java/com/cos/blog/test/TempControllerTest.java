package com.cos.blog.test;

import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.GetMapping;

							// 해당 경로 밑에 있는 파일을 리턴을 해준다.
@Controller     // 데이터를 리턴하는게 아닌 파일을 리턴을 할거기 때문에
public class TempControllerTest {

	// http://localhost:8080/blog/temp/home
	public String tempHome() {
		System.out.println("TempHome");
		
		// 파일리턴 기본경로 : src/main/resources/static
		// 리턴명 : /home.html
		// 풀경로 : src/main/resources/static/home.html
		return "/home.html";
	}
	
	// 이미지파일을 리턴
	// http://localhost:8000/blog/temp/img
	// static폴더에 jsp파일을 넣어두면 찾지 못한다 jsp는 동적인파일 Java파일 ( 컴파일이 일어나야 하는 파일 )
	@GetMapping("/temp/img")
	public String tempImage() {
		return "/Sun.jpg";
	}
	
	// 컴파일이 되어서 올것이다. 톰캣이 이 파일은 JSP파일이니까 자바파일이네
	// WEB서버야 이거는 니가 할수 있는 일이 아니니까. 톰캣 내가 해당 파일을 컴파일해서 html파일로 던져줄게
	// 그럼 WEB 브라우저가 지금처럼 이해할수 있을꺼야
	@GetMapping("/temp/jsp")
	public String tempJsp() {
		System.out.println("prefix : /WEB-INF/views 로 설정");
		System.out.println("suffix : .jsp 로 설정");
		System.out.println("Full Path : /WEB-INF/views/test.jsp");
		return "/test";
	}
}