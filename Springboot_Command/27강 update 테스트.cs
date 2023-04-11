
# JpaUpdate

// Email하고 Password를 수정해볼것이라서 받아야 한다.
// Update는 PUT으로 처리해야합니다. ( ID를 찾을 @GetMapping("/dummy/user/{id}")과 URL이 똑같아도 호출 방법이 달라서 괜찮다 )
// JSON 데이터를 받아서 테스트 해볼려면 ( @RequestBody 어노테이션이 필요 )
@Transactional
@PutMapping("/dummy/user/{id}")
public User updateUser(@PathVariable int id,
                       @RequestBody User requestUser) 	//@RequestBody Json 데이터를 요청 => Java Object ( MessageConverter의 Jackson 라이브러리가 변환해서 받아줘요. )
{
    System.out.println("id       : " + id);
    System.out.println("password : " + requestUser.getPassword());
    System.out.println("email    : " + requestUser.getEmail());
    
    // save()함수는 엔티티의 모든 컬럼이 저장이되어야 하기 때문에 Update를 할때는 별로 추천하지 않지만..
    // save()를 사용하여 Update를 할려면 아래 처럼 데이터베이스에 존재하는 유저를 찾아 변경하고자 하는 컬럼을 set으로 지정해줘야한다.
    // orElseThrow : 조건이 맞지않으면 예외를 던진다.
    User user = userRepository.findById(id).orElseThrow(()->{
            return new IllegalArgumentException("수정에 실패 하였습니다.");
    });
    
    // 변경 요청한 paassword와 email을 user객체에 저장
    user.setPassword(requestUser.getPassword());
    user.setEmail(requestUser.getEmail());
    
    // Save()함수는 id를 전달하지 않으면 insert를 해주고,
    // Save()함수는 id를 전달하면 해당 id에 대한 데이터가 있으면 update를 하고
    // save()함수는 id를 전달하면 해당 id에 대한 데이터가 없으면 insert를 해요!
    // userRepository.save(user);	
    
 
    // @Transactional을 사용하면 save()함수를 사용하지 않아도 Update를 할수 있다.
    // 더티 체킹 : 데이터베이스 SELECT를 해서 받아와서 값만 변경하고 위에다가 @Transaction만 걸면 Update가 된다.
    return null;
}