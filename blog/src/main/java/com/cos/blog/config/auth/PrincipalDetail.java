package com.cos.blog.config.auth;
import java.util.ArrayList;
import java.util.Collection;
import org.springframework.security.core.GrantedAuthority;
import org.springframework.security.core.userdetails.UserDetails;
import com.cos.blog.model.User;

import lombok.Getter;

// 스프링 시큐리티가 로그인 요청을 가로채서 로그인을 진행하고 완료가 되면 UserDetails 타입의 오브젝트를
// 스프링 시큐리티의 고유한 세션 저장소에 저장을 해준다.
// PrincipalDetail이 저장된다. ( User 가 포함되어 있어야 한다. )
@Getter			// Board에게 User 오브젝트를 전달해 주기 위해 Getter를 생성
public class PrincipalDetail implements UserDetails {
	
		private User user;				// 콤포지션 ( 클래스에 클래스를 품고 있는 것 )

		public PrincipalDetail(User user) {
				this.user = user;
		};
				
		@Override
		public String getPassword() {
			return user.getPassword();
		}

		@Override
		public String getUsername() {
			return user.getUsername();
		}

		// 계정이 만료되지 않았는지 리턴한다 ( true : 만료 안됨 )
		@Override
		public boolean isAccountNonExpired() {
			return true;
		}

		// 계정이 잠겨있는지 안잠겨있는지 확인 ( true : 안잠겨 있음 )
		@Override
		public boolean isAccountNonLocked() {
			return true;
		}

		// 비밀번호가 만료되지 안았는지 리턴한다 ( true : 만료 안됨 )
		@Override
		public boolean isCredentialsNonExpired() {
			return true;
		}

		// 계정 활성화가 되어 있는지 안되어있는지 리턴 ( true = 활성회 )
		@Override
		public boolean isEnabled() {
			return true;
		}
		
		// 계정의 권한을 리턴한다.
		// GrantedAuthority를 상속한 Collection이여 한다.
		// 계정이 갖고 있는 권한 목록을 리턴한다. ( 권한이 여러개 있을 수 있어서 루프를 돌아야 하는데 우리는 한개만 )
		@Override
		public Collection<? extends GrantedAuthority> getAuthorities() {
			
			// 왜 ArrayList인가?
			// List Collection을 상속을 받는게 있어서
			Collection<GrantedAuthority> collectors = new ArrayList<>();
			
			// 람다식
			collectors.add(()-> { return "ROLL_" + user.getRole(); });
			return collectors;
		}
}