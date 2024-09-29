const path = require('path');
const HtmlWebpackPlugin = require('html-webpack-plugin');
const MiniCssExtractPlugin = require('mini-css-extract-plugin');
const glob = require('glob');

const entries = {};
const chunks = [];
glob.sync('./src/js/**/*.js').forEach((name) => {
    const n = name.slice(name.lastIndexOf('js/') + 3, name.length - 3);
    entries[n] = [name];
    chunks.push(n);
});

const config = {
    entry: {
        ...entries,
        vendor: [
            'bootstrap',
            'bootstrap-datepicker',
            'axios',
            'moment'
        ]
    },
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
                loader: 'expose-loader',
                options: {
                    exposes: ['$', 'jQuery']
                }
            },
            {
                test: path.resolve(__dirname, 'node_modules/popper.js/dist/popper.js'),
                loader: 'expose-loader',
                options: {
                    exposes: ['popper']
                }
            },
            {
                test: path.resolve(__dirname, 'node_modules/moment'),
                loader: 'expose-loader',
                options: {
                    exposes: ['moment']
                }
            },
            {
                test: path.resolve(__dirname, 'node_modules/pnotify/src/'),
                loader: 'expose-loader',
                options: {
                    exposes: ['PNotify']
                }
            },
            {
                test: /\.vue$/,
                use: 'vue-loader',
            },
            {
                enforce: 'pre',
                test: /\.(js|vue)$/,
                use: 'eslint-loader',
                exclude: /node_modules/,
            },
            {
                test: /\.js$/,
                use: 'babel-loader',
                exclude: /node_modules/,
            },
            {
                test: /\.(png|jpe?g|gif)$/,
                use: [
                    {
                        loader: 'url-loader',
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
                        loader: 'url-loader',
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
                        loader: 'url-loader',
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
        new HtmlWebpackPlugin({
            template: './src/index.html',
            filename: '../index.html',
            inject: 'body'
        }),
        new MiniCssExtractPlugin({
            filename: 'css/[name].css',
        }),
    ],
    optimization: {
        splitChunks: {
            cacheGroups: {
                vendor: {
                    test: /[\\/]node_modules[\\/]/,
                    name: 'vendor',
                    chunks: 'all',
                    minChunks: chunks.length * 0.2,
                }
            }
        }
    }
};

module.exports = config;
