
# id로 select 테스트 하기

// { id } 주소로 파라미터를 전달 받을 수 있습니다.
// http://localhost:8000/blog/dummy/user/3
// @PathVariable Url에 파라미터를 넣는다.
@GetMapping("/dummy/user/{id}")
public User detail(@PathVariable int id){

    // 아래의 두 가지 방식중 하나를 선택해서 구현을 하면 된다. 
    // 1. Supplier를 사용하고 lllegalArgumentException()을 Override를 해서 메소드를 재정의해서 구현
    // 2. Supplier를 사용하고 람다식을 이용하여 익명 메소드로 구현하는 방법
    User user = UserRepository.findById(id);

    return user;
} 

# Supplier :
=> Supplier는 Java에서 제공하는 함수형 인터페이스 중 하나로, 매개변수가 없고 리턴 타입이 있는 메서드를 나타내는 인터페이스 입니다.
   Supplier의 리턴 타입은 제네릭으로 지정되며, get() 메서드를 호출하여 해당 타입의 값을 반환 합니다.
   예를 들어 Supplier<String> 인터페이스를 구현한 객체의 get() 메서드를 호출하면 String 타입의 값을 반환합니다.

   Supplier 인터페이스는 자바 8에서 추가되었으며, 함수형 프로그래밍에서 주로 사용됩니다.
            함수형 인터페이스는 람다식으로 구현할 수 있기 때문에 간결하고, 가독성이 높은 코드를 작성할 수 있습니다.;

# Supplier 사용 : 
    new Supplier<IllegalArgumentException>(){
        @Override
        public IllegalArgumentException get() {
            // TODO Auto-generated method stub
            return new IllegalArgumentException(" Supplier<lllegalArgumentExeption>()의 해당 유저의 정보는 없습니다. id : " + id);
        } 
    }
  Supplier 람다식 사용 : new Supplier<IllegalArgumentException>(()->{
        return new lllegalArgumentExeption("람다식 : 해당 정보가 없습니다. ")
  });

# orElseThrow()
orElseThrow()는 Java 8에서 추가된 기능중 하나 입니다.
이 기능은 Optional 클래스와 함께 사용되며, Optional 객체가 비어있을 경우 예외를 발생시킵니다.

Optional은 null을 반환하는 대신에 Optional 객체를 반환하도록 설계된 클래스 입니다.
따라서 Optinal 객체를 사용하면 null 체크를 수행하지 않아도 되며, 이 코드의 가독성이 향상 됩니다.

하지만 Optional 객체를 사용할 때, 객체가 비어있을 경우에 대한 처리가 필요합니다.
이때 orElseThrow()를 사용하면, 
[ Optional 객체가 비어있을 경우 : 원하는 예외를 발생 시킬 수 있습니다. ];

# orElseThrow 사용 예제 :
User user = userRepository.findById(id).orElseThrow(new Supplier<IllegalArgumentException>() {
        @Override
        public IllegalArgumentException get() {
            // TODO Auto-generated method stub
            return new IllegalArgumentException(" Supplier<lllegalArgumentExeption>()의 해당 유저의 정보는 없습니다. id : " + id);
        }
    });

# 람다식을 사용하여 supplier 안써도 되는 경우
User user1 = userRepository.findById(id).orElseThrow(()-> {
				return new IllegalArgumentException("람다식 표현 : 해당 유저의 정보는 없습니다. id : " + id);
			});
			return user1;