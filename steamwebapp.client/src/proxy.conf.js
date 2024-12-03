const { env } = require('process');

const target = env.ASPNETCORE_HTTPS_PORT ? `https://localhost:${env.ASPNETCORE_HTTPS_PORT}` :
    env.ASPNETCORE_URLS ? env.ASPNETCORE_URLS.split(';')[0] : 'https://localhost:7100';

const PROXY_CONFIG = [
  {
    context: [
      "/api/**",
    ],
    target: "http://localhost:7100",
    changeOrigin: true,
    secure: false,
    pathRewrite: {
        "^/api": ""
    }
  }
]

module.exports = PROXY_CONFIG;
