
# 삭제하기 테스트

// 데이터 삭제
@DeleteMapping("/dummy/user/{id}")
public String delete(@PathVariable int id) {
    try {
        userRepository.deleteById(id);
    } catch (EmptyResultDataAccessException e) {
        return "삭제를 실패했습니다. 해당 ID : " + id + "가 데이터베이스에 없습니다.";
    }
    return "ID : " + id + "번이삭제되었습니다.";
}