
# JSON : 타입이 뭔지 어떤 ContentType로 변형을 해야한는지 알아보기
# JSON : ContentType = application/json으로 받아줘야한다.
# JSON : 모든 사람들은 데이터를 전송 시에 JSON으로 변환,
#        모든 사람들은 데이터를 응답 받을 시 JSON으로 변환 해서 받으면 된다.


// JSON은 데이터 통신간의 중간 데이터 역활을 해준다.


자바라는 언어로 프로그래밍을 개발 
    데이터 전송 => 받는이가 자바   ( 받을 수 있음 )
    데이터 전송 => 받는이가 파이썬 ( 받을 수 없음 )

JSON을 사용 :
자바라는 언어로 프로그래밍 개발
  데이터 전송 => ( JSON으로 변환 ) => 받는이가 자바   ( 받을 수 있음 ) == JSON을 자바로 변환
  데이터 전송 => ( JSON으로 변환 ) => 받는이가 파이썬 ( 받을 수 있음 ) == JSON을 파이썬으로 변환

통신을 할 때 : 자바 오브젝트를 -> JSON(변환해서 전송)   응답 받을 떄 : 자바로 변환
              HTML데이터를 -> JSON( 변환해서 전송)     응답 받을 때 : 자바로 변환

@Controller에서 request 요청할 때 head와 body를 JSON으로 변환 ( MINE 타입을 Json )
                response 응답을 받을 때 head와 body를 JSON으로 변환 ( MINE 타입을 Json ) 