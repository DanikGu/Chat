const { env } = require('process');

const target = env.ASPNETCORE_HTTPS_PORT ? `https://localhost:${env.ASPNETCORE_HTTPS_PORT}` :
  env.ASPNETCORE_URLS ? env.ASPNETCORE_URLS.split(';')[0] : 'http://localhost:14670';

const PROXY_CONFIG = [
  {
    context: [
      "/Login/SignIn",
      "/Login/SignUp",
      "/Login/Logout",
      "/Message/SendMessage",
      "/Message/ReciveLastMessages",
      "/Message/ReciveMessages"
    ],
    target: target,
    secure: false
  }
]

module.exports = PROXY_CONFIG;
