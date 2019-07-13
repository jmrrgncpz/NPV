modules.exports = {
    devServer : {
        proxy : {
            '^api' : {
                target : "http://localhost:59400/",
                ws : true,
                changeOrigin : true
            }
        }
    }
}