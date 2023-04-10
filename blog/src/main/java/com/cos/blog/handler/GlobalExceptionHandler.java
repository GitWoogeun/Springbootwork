package com.cos.blog.handler;

import org.springframework.web.bind.annotation.ControllerAdvice;
import org.springframework.web.bind.annotation.ExceptionHandler;
import org.springframework.web.bind.annotation.RestController;

//모든 Exception이 발생하면 GlobalExceptionHandler 클래스로 들어온다.
@ControllerAdvice   
@RestController
public class GlobalExceptionHandler {
	
		// Spring이 모든 Exception을 받아서 GlobalExceptionHandler 클래스에 보내준다.
		@ExceptionHandler(value = Exception.class)
		public String handleArgumentException( Exception e ) {
			return "<h1>" + e.getMessage() + "</h1>";
		}
}
