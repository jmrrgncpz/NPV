modules.exports = {
    devServer : {
        proxy : {
            '^api' : {
                target : "http://localhost:52612",
                ws : true,
                changeOrigin : true
            }
        }
    }
}