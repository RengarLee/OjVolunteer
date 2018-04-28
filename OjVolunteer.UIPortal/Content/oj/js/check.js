layui.use(['form','layer'],function(){
		var form = layui.form;
		form.verify({
		  	userID: function(value, item){ //value：表单的值、item：表单的DOM对象
		    if(!new RegExp("^[a-zA-Z0-9_s·]+$").test(value)){
		      return '登录帐号不能有特殊字符和中文字符';
		    }
		    if(/(^\_)|(\__)|(\_+$)/.test(value)){
		      return '登录帐号首尾不能出现下划线\'_\'';
		    }
		    if(/^\d+\d+\d$/.test(value)){
		      return '登录帐号不能全为数字';
		    }
		    if(! /{6,18}$/.test(value)){
		    	return '登录帐号必须在6到18位之间'
		    }
		  }
		  //数组的两个值分别代表：[正则匹配、匹配不符时的提示文字]
		  ,pass: [
		    /^[\S]{6,18}$/
		    ,'密码必须在6到18位之间，且不能出现空格'
		  ]
		  ,username:[
		   	/^[\u4e00-\u9fa5]{2,8}$/
		   	,'姓名必须在2到8个汉字之间'
		  ]
		  ,studentNumber:[
		  	/^[0-9]{11}$/
		  	,'学号必须是数字，且只能是11位'
		  ]
		});      
});