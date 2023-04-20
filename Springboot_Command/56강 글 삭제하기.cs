#┌─────────────────────────────────────────────────────────────────────────────────
#│ 글 삭제하기
#└─────────────────────────────────────────────────────────────────────────────────

#┌─────────────────────────────────────────────────────────────────────────────────
#│ details.jsp
#└─────────────────────────────────────────────────────────────────────────────────
<%@ page language="java" contentType="text/html; charset=UTF-8" pageEncoding="UTF-8"%>
// 위치가 board이기 때문에 한칸 뒤로 가서 loayout/header.jsp로 이동해야 한다.
<%@ include file="../layout/header.jsp"%>
// div class container : container는 header 영역과 footer 영역 안에 포함되어있는 공간
<div class="container">
    <br><br>
    <div>
        <b>글 번호 : <span id="id"><i>${board.id} </i></span><br></b>
        <b>글 작성자 : <span><i>${board.user.username} </i></span></b>
    </div>
    <br>
    <div>
        <h3>${board.title}</h3>
    </div>
    <hr>
    <div>
        <div>${board.content}</div>
    </div>
    <hr>
    <button class="btn btn-info" onclick="history.back()">돌아가기</button>
    <button id="btn-update" class="btn btn-success">수정</button>
    // board의 userId와 principal의 userId가 같을 때만 삭제 가능하게
    <c:if test="${board.user.id == principal.user.id}">
        <button id="btn-delete" class="btn btn-danger">삭제</button>
    </c:if>
</div>
// js 폴더 / board.js 파일 연결
<script src="/js/board.js"></script>
// div class container : container는 header 영역과 footer 영역 안에 포함되어있는 공간
<%@ include file="../layout/footer.jsp"%>

#┌─────────────────────────────────────────────────────────────────────────────────
#│ boardApiController
#└─────────────────────────────────────────────────────────────────────────────────
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
}

#┌─────────────────────────────────────────────────────────────────────────────────
#│ boardService
#└─────────────────────────────────────────────────────────────────────────────────
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

#┌─────────────────────────────────────────────────────────────────────────────────
#│ board.js
#└─────────────────────────────────────────────────────────────────────────────────
// 제이쿼리 사용
let index = {
    init: function() {
        // 글쓰기
        $("#btn-save").on("click", ()=>{
                this.save();		// <= 회원가입 버튼을 클릭 했을 시 save: function()을 호출
        });
        
        // 글 삭제
        $("#btn-delete").on("click", ()=>{
                this.deleteById();		// <= 회원가입 버튼을 클릭 했을 시 save: function()을 호출
        });
    },
    
    // 글 쓰기
    save: function(){
        // alert("user의 save함수 호출됨");
        let data = {
            title: $("#title").val(),
            content: $("#content").val(),						
        };
        // 데이터를 잘 가져오는지 확인
        // console.log(data);
        
        $.ajax({
            type: "post",					// POST방식으로 전송
            url: "/api/board",												
            data: JSON.stringify(data),		// JSON 문자열로 data ( http body 데이터 ) 변경 (MIME 타입이 필요)
            contentType: "application/json; charset=utf-8",
            dataType: "json"				// 요청을 서버로해서 응답이 왔을 때 기본적으로 모든 것이 버퍼로 오기 때문에 응답값은 String 입니다.
                                            // dataType : JSON => 생긴게 JSON이라면 => javascript로 오브젝트 변경 
        }).done(function(resp){					    
            // 응답의 결과가 정상이면 done을 실행되는 영역
            console.log(resp);
            alert("글쓰기가 완료 되었습니다.");
            location.href = "/";				 
            
        }).fail(function(error){
            // 응답의 결과가 실패 하면 fail을 실행
            alert(JSON.spstringify(error));
        });
    },
    
    // 글 삭제
    deleteById: function(){
        
        // var id = $("#id").val()가 안돼는 이유
        // val()값을 가져오는게 아닌 text()값을 가져와야하기 때문에
        var id = $("#id").text();
        
        $.ajax({
            type: "delete",								// POST방식으로 전송
            url: "/api/board/"+id,																		
            contentType: "application/json; charset=utf-8",
            dataType: "json"							// 요청을 서버로해서 응답이 왔을 때 기본적으로 모든 것이 버퍼로 오기 때문에 응답값은 String 입니다.
        }).done(function(resp){					    
            // 응답의 결과가 정상이면 done을 실행되는 영역
            console.log(resp);
            alert("글 삭제가 완료 되었습니다.");
            location.href = "/";				 
        }).fail(function(error){
            // 응답의 결과가 실패 하면 fail을 실행
            alert("글 삭제가 되지 않았습니다.");
        });
    },
}

index.init();
