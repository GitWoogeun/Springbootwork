package com.cos.blog.service;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.data.domain.Page;
import org.springframework.data.domain.Pageable;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;

import com.cos.blog.model.Board;
import com.cos.blog.model.User;
import com.cos.blog.repository.BoardRepository;

// 스프링이 컴포넌트 스캔을 통해서 빈에 등록을 해준다.  ( IoC를 해준다 ) 메모리를 대신 띄어준다.
@Service
public class BoardService {
	
		@Autowired
		private BoardRepository boardRepository;
		
		// 글 쓰기
		// javax의 @Transaction
		// 트랜잭션중 하나라도 실패를 한다면 커밋이 되지 않고 RollBack이 될겁니다.
		@Transactional
		public void 글쓰기(Board board, User user) {				// title, content
			board.setCount(0);
			board.setUser(user);
			boardRepository.save(board);
		}
		
		// 페이징 처리
		// Pageable을 리턴을 한다면 리턴 타입을 List가 아닌 Page로 해줘야한다.
		// Page를 리턴 타입으로 지정할 시 이게 첫번째 페이지 인지 마지막 페이지 인지 확인할수 있다.
		@Transactional(readOnly = true)
		public Page<Board> 글목록(Pageable pageable) {
				return boardRepository.findAll(pageable);
		}
		
		// 글 상세보기
		@Transactional(readOnly = true)
		public Board 글상세보기(int id){
				return boardRepository.findById(id)
						.orElseThrow(()-> {
								return new IllegalArgumentException("글 상세보기 아이디를 찾을 수 없습니다.");
						});
		}
		
		
		// 글 삭제하기
		@Transactional
		public void 글삭제하기(int id) {
				boardRepository.deleteById(id);
		}
}