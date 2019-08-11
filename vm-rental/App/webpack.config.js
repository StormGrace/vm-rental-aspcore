const HtmlWebPackPlugin = require('html-webpack-plugin');
const path = require( 'path' );

module.exports = {
 context: __dirname,
 mode: 'development',

 entry: {
  index: './src/index.js',
 },

 output: {
  path: path.resolve( __dirname, 'dist' ),
  publicPath: '/dist/',
  filename: 'main.js',
 },
 module: {
  rules: [
   {
    test: /\.(js|jsx)?$/,
    exclude: /node_modules/,
    use: 'babel-loader'
   }
  ]
 },

 resolve: {
  extensions: ['.js', '.jsx']
 },

 plugins: [
    new HtmlWebPackPlugin({
      template: path.resolve(__dirname, 'public/index.html'),
      filename: 'index.html'
    })
  ]
};