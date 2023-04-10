package com.cos.blog.controller;

import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.GetMapping;

@Controller
public class boardController {

	@GetMapping({"" , "/"})					//아무것도 적지 않았을때랑 /를 붙였을때랑
	public String index() {
		//		/WEB-INF/views/index.jsp
		return "index";
	}
}
