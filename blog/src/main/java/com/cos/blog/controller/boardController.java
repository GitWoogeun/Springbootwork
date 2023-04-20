package com.cos.blog.controller;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.data.domain.Pageable;
import org.springframework.data.domain.Sort;
import org.springframework.data.web.PageableDefault;
import org.springframework.stereotype.Controller;
import org.springframework.ui.Model;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PathVariable;

import com.cos.blog.service.BoardService;

@Controller
public class boardController {
	
	@Autowired
	private BoardService boardService;
	
	// 컨트롤러에서 어떻게 찾지?
	// @AuthenticationPrincipal PrincipalDetail principal
	// @PageableDefault => 페이징처리 ( SIZE = 보여주는 글 개수, SORT = 보여주는 기준, Sort.Direction.DESC = SORT 기준의 내림차순으로 정렬 ) 
	@GetMapping({"" , "/"})					
	public String index(Model model, @PageableDefault(size = 3, sort="id", direction = Sort.Direction.DESC) Pageable pageable) {
		
		// model => Jsp에서 보면 Request 정보 ( View 화면 까지 )	
		model.addAttribute("boards", boardService.글목록(pageable));   
		// @Controller는 리턴할 때 ViewResolve가 작동 !! 해당 index 페이지로 Model을 들고 이동합니다.
		return "index";			
		 
	}
	
	@GetMapping("/board/{id}")
	public String findById(@PathVariable int id, Model model) {
		model.addAttribute("board", boardService.글상세보기(id));
		
		return "board/detail";
	}
	
	// USER 권한이 필요
	@GetMapping("/board/saveForm")
	public String saveForm() {
		return "board/saveForm";
	}
}
