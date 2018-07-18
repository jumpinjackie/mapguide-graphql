import * as React from "react";
import Tree from "antd/lib/tree";
import { DirectoryNode, LIST_FOLDER_ITEMS_QUERY } from "./directory-node";
import { LoginForm } from "./login-form";
import { AppContext } from "../app-context";
import { Query } from "react-apollo";

export interface SiteExplorerProps {
    
}

export interface SiteExplorerState {

}

export class SiteExplorer extends React.Component<SiteExplorerProps, SiteExplorerState> {
    constructor(props: SiteExplorerProps) {
        super(props);
    }
    render(): JSX.Element {
        return <AppContext.Consumer>
            {({ sessionId, onSessionAcquired, client }: any) => {
                if (sessionId) {
                    return <Tree>
                        <Query query={LIST_FOLDER_ITEMS_QUERY} variables={{ sessionId, name: "/" }}>
                            {({ loading, error, data }) => {
                                if (loading) return <Tree.TreeNode title="Loading..." />;
                                if (error) return <Tree.TreeNode title="Error :(" />;

                                const folders = data.folders.map((f: any) => <DirectoryNode key={`/${f.name}`} sessionId={sessionId} parentPath="/" name={f.name} />);
                                const items = data.items.map((f: any) => <Tree.TreeNode key={`/${f.name}`} title={f.name} />);
                                return <>
                                    {folders}
                                    {items}
                                </>;
                            }}
                        </Query>
                    </Tree>;
                } else {
                    return <LoginForm onSessionAcquired={onSessionAcquired} client={client} />;
                }
            }}
        </AppContext.Consumer>;
    }
}