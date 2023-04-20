package com.cos.blog.controller;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Controller;
import org.springframework.ui.Model;
import org.springframework.web.bind.annotation.GetMapping;

import com.cos.blog.service.BoardService;

@Controller
public class boardController {
	
	@Autowired
	private BoardService boardService;
	
	// 컨트롤러에서 어떻게 찾지?
	// @AuthenticationPrincipal PrincipalDetail principal
	@GetMapping({"" , "/"})					
	public String index(Model model) {
			model.addAttribute("boards", boardService.글목록());  // model => Jsp에서 보면 Request 정보 ( View 화면 까지 ) 
		return "index";			// @Controller는 리턴할 때 ViewResolve가 작동 !! 해당 index 페이지로 Model을 들고 이동합니다. 
	}
	
	// USER 권한이 필요
	@GetMapping("/board/saveForm")
	public String saveForm() {
		return "board/saveForm";
	}
}
