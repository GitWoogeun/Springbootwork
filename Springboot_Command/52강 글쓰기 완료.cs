#┌─────────────────────────────────────────────────────────────────────────────────
#│ 글쓰기 완료 
#└─────────────────────────────────────────────────────────────────────────────────


#┌─────────────────────────────────────────────────────────────────────────────────
#│ saveForm.jsp ( 글쓰기 화면 )
#└─────────────────────────────────────────────────────────────────────────────────
<%@ page language="java" contentType="text/html; charset=UTF-8" pageEncoding="UTF-8"%>

// 위치가 board이기 때문에 한칸 뒤로 가서 loayout/header.jsp로 이동해야 한다.
<%@ include file="../layout/header.jsp"%>
// div class container : container는 header 영역과 footer 영역 안에 포함되어있는 공간
<div class="container">
	
	<form action="/auth/loginProc" method="post">
		<div class="form-group">
			<label for="title">제목</label>
			 <input type="text"  placeholder="Enter title" class="form-control"  id="title">
		</div>
		
		<div class="form-group">
  			<label for="content">글 내용</label>
  			<textarea  class="form-control summernote" rows="5"  id="content">
  			
  			</textarea>
		</div>
	</form>
	<button id="btn-save" class="btn btn-primary">글쓰기</button>
</div>

// summernote BootStrap에서 가져옴
<script>
    $('.summernote').summernote({
    tabsize: 2,
    height: 300
    });
</script>

<script src="/js/board.js"></script>
<%@ include file="../layout/footer.jsp"%>

#┌─────────────────────────────────────────────────────────────────────────────────
#│ board.js
#└─────────────────────────────────────────────────────────────────────────────────
// 제이쿼리 사용
let index = {
    init: function() {
        $("#btn-save").on("click", ()=>{
                this.save();		// <= 회원가입 버튼을 클릭 했을 시 save: function()을 호출
        });
    },
    
    save: function(){
        // alert("user의 save함수 호출됨");
        let data = {
            title: $("#title").val(),
            content: $("#content").val(),						
        };
        // 데이터를 잘 가져오는지 확인
        // console.log(data);
        
        $.ajax({
            type: "post",									// POST방식으로 전송
            url: "/api/board",												
            data: JSON.stringify(data),		// JSON 문자열로 data ( http body 데이터 ) 변경 (MIME 타입이 필요)
            contentType: "application/json; charset=utf-8",
            dataType: "json"							// 요청을 서버로해서 응답이 왔을 때 기본적으로 모든 것이 버퍼로 오기 때문에 응답값은 String 입니다.
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
}

index.init();


#┌─────────────────────────────────────────────────────────────────────────────────
#│ BoardController ()
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
    // @AuthenticationPrincipal : 유저의 권한을 전송 ( 유저의 정보를 가져오기 위해, Join )
    @PostMapping("/api/board")
    public ResponseDto<Integer> save(@RequestBody Board board, @AuthenticationPrincipal PrincipalDetail principal) {		
        
        boardService.글쓰기(board, principal.getUser());
        
        // 회원가입이 성공 시 [ 상태값 : 200, 1 ] 호출
        // 자바 오브젝트를 JSON으로 변환해서 리턴 (Jackson)
        return new ResponseDto<Integer>(HttpStatus.OK.value(), 1);	
    }
}