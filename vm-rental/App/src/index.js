import React from 'react';
import ReactDOM from 'react-dom';
import App from './App';
import { hot } from 'react-hot-loader';

const rootElement = document.getElementById('root');

const HotApp = hot(module)(App);

ReactDOM.render(<App />, rootElement);
