const { env } = require('process');

const target = env.apiUrl || 'https://localhost:7203';

const PROXY_CONFIG = [
  {
    context: [
      "/weatherforecast",
    ],
    target,
    secure: false
  }
]

module.exports = PROXY_CONFIG;
