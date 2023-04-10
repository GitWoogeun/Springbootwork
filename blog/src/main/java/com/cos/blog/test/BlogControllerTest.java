package com.cos.blog.test;

import java.util.Collection;
import java.util.SortedMap;

import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RestController;

//스프링이 com.cos.blog 패키지 이하를 스캔해서 모든 파일을 메모리에 new 하는 것은 아니구요
// 어떤 파일을 new 하냐면 특정 어노테이션이 붙어있는 클래스 파일들을 new(IoC) 해서 스프링컨테이너에 관리 해줍니다
// @RestController를 붙여주면 Spring이 시작하면 BlogControllerTest 클래스를 Spring IoC가 Heap 영역에 띄어준다.

@RestController
public class BlogControllerTest  {
	
		@GetMapping("/test/hello")
		public String Hello() {
				return "<h1>Hello Spring Boot</h1>";
		}
}
