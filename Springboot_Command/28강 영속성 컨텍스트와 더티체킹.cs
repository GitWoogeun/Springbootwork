
# 영속성 컨텍스트

Controller에서 
{
    request로 요청을 받잖아요 ( 모든 요청은 컨트롤러로 모인다. )

        insert 저장 => Controller에서 User객체를 하나 만들고 save()를 하잖아요
        update 수정
        select 조회
        delete 삭제

    response로 응답을 해줍니다.
}

# JPA가 들고있는 영속성 컨텍스트가 있다.
{
    1차 캐시
    {
        # 이 안에 들어있는 데이터들은 영속화 되었다고 말할 수 있다.
        # 영속화된 데이터들을 DB에 짚어넣는걸 flash라고 한다.
            # flash : 1차캐시에 꽉 차 있는 데이터를 DB에 저장하고 
            #         다시 1차캐시에 데이터를 짚어 넣을수 있게 비워주는 행위 ( 버퍼를 비운다 )

        User객체가 여기에 다 쌓인다. ( save 할 시 )
            User객체가 하나 만들어져서 User객체로 User테이블로 짚어 넣는다.
            레코드가 3건이 있다고 치면 id가 4가 하나 생기면서 쏙 들어간다.

        특징 : flash긴 한데 비우지 않고 남아있다. 남겨두고 사용하는 방법은 
               Controller에서 Select가 이루어질때 첫번째로 영속화 되었는지 확인
               영속화 되어있다면 굳이 DB에가서 select해서 들고 오지 않고
               1차캐시에서 가지고 옵니다. ( db에 접근하지 않아도 되니까 부하가 덜하다 )    
    }

    # Update를하고 싶을 때
    기존 DB에 있는 데이터를 수정하고 싶다면
    DB에 있는 기존데이터를 SELECT를 해서 1차캐시에 영속화를 시킨다.
    그리고 나서 영속화된 오브젝트에 값을 변경을 한다.
    변경이 끝나고 나서 다시 변경된 유저 오브젝트를 변경된 값으로 변경할려면
    save()를 호출을하게 되면 영속화된 데이터와 비교해 본다.

    그런다음 변경된 내용만 DB에서 가져와서 1차캐시에 보관되어있는 User오브젝트에 적용 시킨다.
    변경된 User 오브젝트를 flash해서 DB에 다시 밀어 넣어둔다면 DB에 저장되어있는 User 오브젝트가
    변경이 된다.


    Controller
    {
        @Transaction
        update() {
           DB에 있는 2번 데이터를 영속성 컨텍스트에 가져와요! ( 1차캐시라는 곳에 )
           2번 데이터는 영속화 되었다고 합니다. ( 영속화된 데이터를 Controller에서 가져와서 )
           Controller에서 password랑 email을 변경을하고
           Controller가 종료가 되었는데 @Transaction이 있으니까 종료시 commit이 된다.

            commit이 되면 원래 처음에 있던 User 오브젝트의 password와 email을 비교를해서
            변경이되었구나 자동으로 인식을해요!
            변경이 되었을 시 Controller가 종료가 되었을 때 flash로 DB에 밀어넣습니다.
            이때 Update가 수행이 된다.

            return;
        }-> return(종료 시) commit이 된다.
    }   그런다음 DB에 변경된 내용을 업데이트 ( flash를 해준다. )
} 