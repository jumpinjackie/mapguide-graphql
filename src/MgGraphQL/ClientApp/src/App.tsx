import * as React from 'react';
import './App.css';
import Layout from "antd/lib/layout";
import Menu, { SelectParam } from "antd/lib/menu";
import Icon from "antd/lib/icon";
import ApolloClient from "apollo-boost";
import { ApolloProvider } from "react-apollo";
import { SiteExplorer } from './components/site-explorer';
import { AppContext } from './app-context';

const client = new ApolloClient({
    uri: "/api/graphql"
});

const TABS = [
    { 
        title: "Site Explorer", 
        icon: <Icon type="database" />,
        content: <SiteExplorer />
    },
    { 
        title: "Server Status", 
        icon: <Icon type="area-chart" /> 
    }
];

interface AppProps {

}

interface AppState {
    sessionId: string | undefined;
    activeTabKey: string;
}

class App extends React.Component<AppProps, AppState> {
    constructor(props: AppProps) {
        super(props);
        this.state = {
            sessionId: undefined,
            activeTabKey: "0"
        }
    }
    private onTabSelect = (e: SelectParam) => {
        this.setState({
            activeTabKey: e.key
        });
    }
    private onSessionAcquired = (session: string) => {
        this.setState({
            sessionId: session
        });
    }
    public render() {
        const { sessionId, activeTabKey } = this.state;
        return <ApolloProvider client={client}>
            <AppContext.Provider value={{ sessionId, onSessionAcquired: this.onSessionAcquired, client }}>
                <Layout style={{ height: "100%" }}>
                    <Layout>
                        <Layout.Sider collapsed={true}>
                            <Menu theme="dark" mode="inline" selectedKeys={[activeTabKey]} onSelect={this.onTabSelect}>
                                {TABS.map((t, i) => (<Menu.Item key={`${i}`}>
                                    {t.icon}
                                    <span>{t.title}</span>
                                </Menu.Item>))}
                            </Menu>
                        </Layout.Sider>
                        <Layout.Sider width={320}>
                            <Layout.Content style={{ background: '#fff', height: "100%" }}>
                                {TABS[activeTabKey].content}
                            </Layout.Content>
                        </Layout.Sider>
                        <Layout>
                            <Layout.Content style={{ background: '#fff', height: "100%" }}>
                                BODY
                            </Layout.Content>
                        </Layout>
                    </Layout>
                </Layout>
            </AppContext.Provider>
        </ApolloProvider>;
    }
}

export default App;
