package com.cos.blog.model;

import lombok.Data;

// 메인 파일이기 때문에 public이 붙어 있어야 한다.
// 중요! => KakaoDevelopers에서 아래 항목들을 전부 체크 해야한다. ( 항목 동의를 하지 않을 시 NullPointException이 발생!! )
@Data
public class KakaoProfile {

	public Long id;
	public String connected_at;
	public Properties properties;
	public KakaoAccount kakao_account;

	@Data
	public class Properties {
		
		public String nickname;
		public String profile_image;
		public String thumbnail_image;
	}

	@Data
	public class KakaoAccount {

		public Boolean profile_nickname_needs_agreement;
		public Boolean profile_image_needs_agreement;
		public Boolean profile_needs_agreement;
		public Profile profile;
		public Boolean has_email;
		public Boolean email_needs_agreement;
		public Boolean is_email_valid;
		public Boolean is_email_verified;
		public String email;

		@Data
		public class Profile {

			public String is_default_image;
			public String nickname;
			public String thumbnail_image_url;
			public String profile_image_url;
		}

	}
}