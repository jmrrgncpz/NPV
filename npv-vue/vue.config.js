modules.exports = {
    devServer : {
        proxy : {
            '^api' : {
                target : "http://localhost:56917",
                ws : true,
                changeOrigin : true
            }
        }
    }
}