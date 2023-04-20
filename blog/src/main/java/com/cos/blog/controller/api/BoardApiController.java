package com.cos.blog.controller.api;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.security.core.annotation.AuthenticationPrincipal;
import org.springframework.web.bind.annotation.DeleteMapping;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.PutMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RestController;

import com.cos.blog.config.auth.PrincipalDetail;
import com.cos.blog.dto.ResponseDto;
import com.cos.blog.model.Board;
import com.cos.blog.service.BoardService;

// 얘는 나중에 웹에도 쓸수 있다.
//데이터만 리턴을 해주는 컨트롤러 이기 때문에 @RestController
@RestController						
public class BoardApiController {

		@Autowired
		private BoardService boardService;
	
		// JSON이니까 @RequestBody로 파라미터 받음
		// 통신상태를 확인하기 위해 HttpStatus.OK 
		// board.js의 $Ajax에게 데이터 요청
		@PostMapping("/api/board")
		public ResponseDto<Integer> save(@RequestBody Board board, @AuthenticationPrincipal PrincipalDetail principal) {		
			
			boardService.글쓰기(board, principal.getUser());
			
			// 회원가입이 성공 시 [ 상태값 : 200, 1 ] 호출
			// 자바 오브젝트를 JSON으로 변환해서 리턴 (Jackson)
			return new ResponseDto<Integer>(HttpStatus.OK.value(), 1);	
		}
		
		@DeleteMapping("/api/board/{id}")
		public ResponseDto<Integer> deleteById(@PathVariable int id){
				boardService.글삭제하기(id);
				return new ResponseDto<Integer>(HttpStatus.OK.value(), 1);
		}
		
		// Delete 랑 URL 주소가 같아도 상관없다 요청 Method가 다르기 때문에
		// 시큐리티의 ID와 Board의 정보를 매개변수로 보낸다.
		@PutMapping("/api/board/{id}")
		public ResponseDto<Integer> update(@PathVariable int id, @RequestBody Board board) {
				boardService.글수정하기(id, board);
				return new ResponseDto<Integer>(HttpStatus.OK.value(),1);
		}
}