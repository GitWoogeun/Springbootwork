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
		
		// 글 수정하기
		// 수정된 값을 매개변수로 전달
		@Transactional
		public void 글수정하기(int id, Board requestBoard)
		{
				// 영속화
				Board board = boardRepository.findById(id)
						.orElseThrow(()-> {
							return new IllegalArgumentException("글 수정을 실패 : 아이디를 찾을수 없습니다.");
					}); // 영속화 완료
				board.setTitle(requestBoard.getTitle());
				board.setContent(requestBoard.getContent());
				// 해당 함수가 종료 시 Service가 종료 될 때 트랜잭션이 종료 됩니다. 이 때 더티체킹이 일어난다.
				// 자동 업데이트 된다 flush가 된다 -> DB로
		}
}