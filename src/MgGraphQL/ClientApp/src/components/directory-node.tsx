import * as React from "react";
import gql from "graphql-tag";
import { Query } from "react-apollo";
import Tree from "antd/lib/tree";

export const LIST_FOLDER_ITEMS_QUERY = gql`
    query EnumerateResources ($name: String!, $sessionId: String!) {
        resources (options: { sessionId: $sessionId, path: $path }) {
            data {
                folders {
                    name
                }
                items {
                    name
                }
            }
        }
    }
`;

export interface DirectoryNodeProps {
    name: string;
    parentPath: string;
    sessionId: string;
}

export interface DirectoryNodeState {
    loadedChildren: boolean;
}

export class DirectoryNode extends React.Component<DirectoryNodeProps, DirectoryNodeState> {
    constructor(props: DirectoryNodeProps) {
        super(props);
        this.state = {
            loadedChildren: false
        }
    }
    render(): JSX.Element {
        const { parentPath, ...vars } = this.props;
        if (!this.state.loadedChildren) {
            return <Tree.TreeNode title={vars.name} />
        } else {
            return <Query query={LIST_FOLDER_ITEMS_QUERY} variables={vars}>
                {({ loading, error, data }) => {
                    if (loading) return <Tree.TreeNode title="Loading..." />;
                    if (error) return <Tree.TreeNode title="Error :(" />;

                    const folders = data.folders.map((f: any) => <DirectoryNode key={`${parentPath}${vars.name}${f.name}`} sessionId={vars.sessionId} parentPath={`${parentPath}/${vars.name}/`} name={f.name} />);
                    const items = data.items.map((f: any) => <Tree.TreeNode key={`${parentPath}${vars.name}${f.name}`} title={f.name} />);
                    return <>
                        {folders}
                        {items}
                    </>;
                }}
            </Query>
        }
    }
}