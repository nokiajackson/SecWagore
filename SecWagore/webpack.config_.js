// <binding ProjectOpened='Run - Development' />
const path = require('path');
const webpack = require('webpack');
const glob = require('glob');
const MiniCssExtractPlugin = require('mini-css-extract-plugin');
const OptimizeCSSAssetsPlugin = require('optimize-css-assets-webpack-plugin');
const CleanWebpackPlugin = require('clean-webpack-plugin');
const UglifyJsPlugin = require('uglifyjs-webpack-plugin');

const entries = {};
const chunks = [];
glob.sync('./src/js/**/*.js').forEach((name) => {
    const n = name.slice(name.lastIndexOf('js/') + 3, name.length - 3);
    entries[n] = [name];
    chunks.push(n);
});

const config = {
    entry: entries,
    output: {
        path: path.resolve(__dirname, './wwwroot/Content'),
        filename: 'js/[name].js',
        publicPath: '/Content/',
        sourceMapFilename: 'js/[name].js.map',
    },
    resolveLoader: {
        moduleExtensions: ['-loader'],
    },
    resolve: {
        extensions: ['.js', '.vue', '.sass', '.css'],
        alias: {
            src: path.resolve(__dirname, './src'),
            components: path.resolve(__dirname, './src/components'),
            content: path.resolve(__dirname, './wwwroot/Content'),
            root: path.join(__dirname, 'node_modules'),
        },
    },
    module: {
        rules: [
            {
                test: path.resolve(__dirname, 'node_modules/jquery/dist'),
                loader: 'expose?jQuery!expose?$',
            },
            {
                test: path.resolve(__dirname, 'node_modules/popper.js/dist/popper.js'),
                loader: 'expose?popper',
            },
            {
                test: path.resolve(__dirname, 'node_modules/moment'),
                loader: 'expose?moment',
            },
            {
                test: path.resolve(__dirname, 'node_modules/pnotify/src/'),
                loader: 'expose?PNotify',
            },
            {
                test: /\.vue$/,
                use: 'vue',
            },
            {
                enforce: 'pre',
                test: /\.(js|vue)$/,
                use: 'eslint',
                exclude: /node_modules/,
            },
            {
                test: /\.js$/,
                use: 'babel',
                exclude: /node_modules/,
            },
            {
                test: /\.(png|jpe?g|gif)$/,
                use: [
                    {
                        loader: 'url',
                        options: {
                            limit: 10000,
                            name: 'images/[name].[ext]',
                        },
                    },
                ],
            },
            {
                test: /\.(sass|css)$/,
                use: [
                    {
                        loader: MiniCssExtractPlugin.loader,
                        options: {
                            minimize: true,
                            sourceMap: true,
                        },
                    },
                    'css-loader',
                    'sass-loader',
                ],
            },
            {
                test: /\.ico$/,
                exclude: /node_modules/,
                use: [
                    {
                        loader: 'url',
                        options: {
                            limit: 1,
                            name: 'images/[name].[ext]',
                        },
                    },
                ],
            },
            {
                test: /\.(eot|ttf|otf|woff|woff2|svg|svgz)$/,
                use: [
                    {
                        loader: 'url',
                        options: {
                            limit: 10000,
                            name: 'fonts/[name].[ext]',
                        },
                    },
                ],
            },
        ],
    },
    plugins: [
        new CleanWebpackPlugin(['wwwroot/Content'], {
            exclude: ['images', 'lib'],
        }),
        new MiniCssExtractPlugin({
            filename: 'css/[name].css',
            allChunks: true,
        }),
        new webpack.LoaderOptionsPlugin({
            debug: true,
        }),
        // new webpack.ProvidePlugin({
        //    Vue: ['vue/dist/vue.esm.js', 'default'],
        //    jQuery: 'jquery',
        //    'window.jQuery': 'jquery',
        //    $: 'jquery',
        //    moment: 'moment',
        //    Popper: ['popper.js', 'default'] //4.0.0-beta
        // }),
    ],
    optimization: {
        splitChunks: {
            cacheGroups: {
                commons: {
                    test: /[\\/]node_modules[\\/]/,
                    name: 'vendors',
                    chunks: 'all',
                    minChunks: chunks.length * 0.2,
                },
            },
        },
    },
    devServer: {
        // host: '127.0.0.1', //for frontend, run server
        // port: 8010, //for frontend, run server
        contentBase: path.resolve(__dirname, './'),
        // contentBase: 'http://localhost:9863', // for .NET MVC, run dev
        historyApiFallback: false,
        noInfo: true,
    },
    devtool: 'eval-cheap-module-source-map', // 'eval-cheap-module-source-map'
};

module.exports = config;

if (process.env.NODE_ENV === 'production') {
    module.exports.devtool = false;
    module.exports.optimization.minimizer = [
        new UglifyJsPlugin({ uglifyOptions: { compress: true } }),
        // new OptimizeCSSAssetsPlugin(),
    ];
}
