import apiCall from './apiCall'

export const userRegistry = function (opt) {
  let URL = "/v1/auth/createRegister";
  return apiCall(opt, URL);
}

export const getVcCode = function (opt) {
  let URL = "/v1/auth/obtainCode";
  return apiCall(opt, URL);
}

export const userLogin = function (opt) {
  let URL = "/v1/auth/userLogin";
  return apiCall(opt, URL);
}

export const userForget = function (opt) {
  let URL = "/v1/auth/resetPassword";
  return apiCall(opt, URL);
}

export const confirmCode = function (opt) {
  let URL = "/v1/auth/confirmCode";
  return apiCall(opt, URL);
}

export const qrcodeLogin = function (opt) {
  let URL = "/v1/web/login";
  return apiCall(opt, URL);
}
